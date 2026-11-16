# CLAUDE.md

2D 팩토리/시뮬레이션 게임 프로젝트 (Unity). 이 문서는 프로젝트 전반에 공통으로 적용되는 구조와 규칙만 정리한다.

## 환경

- Unity **6000.3.10f1**, URP 2D (`com.unity.render-pipelines.universal` 17.3)
- 주요 패키지: Tilemap / Tilemap Extras, **New Input System** (1.18), 2D Animation, Test Framework
- 빌드/테스트 CLI 없음 — 컴파일 확인과 실행은 Unity 에디터에서 한다. 루트의 `*.csproj`, `*.sln`은 Unity가 생성하는 파일이며 일부(BattleSystem, SoundSystem 등)는 현재 Assets에 없는 오래된 잔재다.
- 씬: `Assets/Scenes/` (`SampleScene`, `AstarTest`, `SaveTest`)

## 폴더 구조

| 경로 | 내용 |
|---|---|
| `Assets/_Script/` | 게임 코드 (기본 `Assembly-CSharp`) |
| `Assets/_Script/_Core/` | 공통 인프라: 이벤트, FSM, 모듈, 서비스 로케이터, 매니저, 로딩, 입력 |
| `Assets/_Script/_<System>/` | 기능별 시스템 (`_MapSystem`, `_2DPlatformAstar`, `_Agent`, `_SaveSystem` …) |
| `Assets/_Script/_Test/` 및 각 시스템의 `_Test`/`Test` 폴더 | 테스트·디버그용 코드 |
| `Assets/_GameModule/Core/` | `InputController.inputactions` 및 자동 생성된 `InputController.cs` |
| `Assets/LrwLib/` | 자체 유틸 라이브러리 (`UnityButton` 어트리뷰트, TCP 서버, Excel 임포터) |
| `Assets/DevLib/PolyNavMesh/` | 외부 폴리곤 NavMesh 라이브러리 (별도 asmdef) |
| `SaveData/` (프로젝트 루트) | `SaveManager`가 쓰는 세이브 파일 위치 |

asmdef: `_Core/ModuleSystem`(`ModuleSystem_Assembly`), `LrwLib/Server`(`UnityServer`), `DevLib/PolyNavMesh`(`PolyNavMesh_assembly`). 나머지는 `Assembly-CSharp`. asmdef 안의 코드는 `Assembly-CSharp`의 타입을 참조할 수 없으니 주의.

## 코딩 규칙

- **네임스페이스 = 폴더 경로** 그대로 (언더스코어 포함). 예: `Assets/_Script/_MapSystem/_Map/_TileMap` → `namespace _Script._MapSystem._Map._TileMap`
- 블록 네임스페이스(`namespace X { }`) 사용, 파일당 하나의 주요 타입.
- 네이밍
  - private 필드: `_camelCase`
  - 인스펙터 노출 필드: `[SerializeField] private Type camelCase;`
  - ScriptableObject 데이터: `[field: SerializeField] public Type Name { get; private set; }`
  - public/readonly 필드, 프로퍼티, 상수, 메서드: `PascalCase`
  - 인터페이스 `I` 접두사, 추상 클래스 `Abstract` 접두사 (`AbstractManager`, `AbstractTile`, `AbstractMonoState`)
- target-typed `new()` 적극 사용.
- 주석은 한국어.
- 필수 참조 검증은 `Debug.Assert` 또는 `FDebug.Assert`.
- 에디터 전용 코드는 `#if UNITY_EDITOR`로 감싸거나 `Editor/` 폴더에 둔다.
- `Assets/_GameModule/Core/InputController.cs`는 자동 생성 파일 — **직접 수정 금지**, `.inputactions`를 수정해 재생성한다.

## 공통 인프라 (`_Script/_Core`)

- **EventBus** (`_EventSystem/EventBus.cs`): 정적 제네릭 이벤트 버스 `EventBus<T>.Event += / -=`, `EventBus<T>.Invoke(evt)`.
  - 이벤트는 `IEvent`를 구현한 `readonly struct`, 이름은 `XxxEvent`, 시스템별 `XxxEvents.cs`에 모아 둔다 (예: `_MapSystem/MapEvents.cs`).
  - 구독은 `Awake`/`OnEnable`, 해제는 `OnDestroy`/`OnDisable`에서 반드시 짝을 맞춘다.
- **EventChannel** (`_EventSystem/EventChannel.cs`): ScriptableObject 기반 이벤트 채널 (`AddListener<T>`, `RemoveListener<T>`, `RaiseEvent`). 에셋 단위로 범위를 나눌 때 사용.
- **ServiceLocator** (`_ServiceLocator/SeviceLoactor.cs`): `ServiceLocator.Register<IInterface>(this)`, `ServiceLocator.Get<IInterface>()`. 인터페이스 타입으로 등록한다.
  - Null Object 패턴: `RuntimeInitializeOnLoadMethod(SubsystemRegistration)`로 `NullXxx`를 기본 등록하고, 실제 서비스가 파괴될 때 다시 Null로 되돌린다 (`_2DPlatformAstar/Surface/NullNavSurface.cs` 참고).
- **ModuleSystem** (`ModuleSystem/`): `ModuleOwner`(MonoBehaviour)가 `Awake`에서 자식의 모든 `IModule`을 수집 → `Initialize(owner)` → `AfterInitialize()` 순으로 호출. 모듈 조회는 `owner.GetModule<T>()` (인터페이스로도 조회 가능). `Agent`가 `ModuleOwner`를 상속한다.
- **FSM** (`_FSM/`): `IState`(`Enter`/`StateUpdate`/`StateFixedUpdate`/`Exit`) + `StateMachine<TKey>`. MonoBehaviour 상태는 `_Agent/_Modules/_FSMModule`의 `MonoFsmModule` + `AbstractMonoState`를 사용 (자식 오브젝트의 상태를 타입 키로 자동 등록, 시작 상태는 커스텀 인스펙터에서 선택).
- **Manager** (`_Manager/`): `AbstractManager`를 상속하면 `GameManager`가 `TypeCache`로 자동 수집해 자식 GameObject로 생성 후 `Initialize()` 호출. 매니저를 씬에 수동 배치할 필요 없음.
- **Loader** (`_Loading/`): `ILoading`(`GetLoadingValue()` 0~100, `Complete()`)을 묶어 평균 진행도를 계산.
- **Input** (`_Input/InputSo.cs`): `InputController.IPlayerActions`를 구현한 ScriptableObject. 입력은 이 SO를 참조해 `MoveDir`, `MovePos`, `MouseDown` 등으로 받는다.
- **AbbClass**: `IEnumerable<T>.Foreach(action)` 확장 메서드.

## 공통 유틸

- **FDebug** (`_Script/_Test/FDebug.cs`): 에디터에서만 출력되는 로그/Assert. 빌드에 남지 않아야 하는 디버그 로그는 `Debug.Log` 대신 이것을 사용.
- **SaveManager** (`_SaveSystem/SaveManager.cs`): 정적 클래스, `<프로젝트>/SaveData/` 하위에 `WriteFile(path, text)` / `ReadFile(path, default)`. 실패 시 예외를 던지지 않고 기본값 반환.
- **`[UnityButton("Label")]`** (`LrwLib.ButtonAttribute`): 매개변수 없는 메서드에 붙이면 인스펙터에 버튼 표시 (MonoBehaviour/ScriptableObject 모두). 테스트용 실행 트리거에 사용. `[ContextMenu]`도 혼용됨.

## 패턴 메모

- 설정 데이터는 ScriptableObject로 분리하고 `[CreateAssetMenu(menuName = "<카테고리>/<이름>")]` 사용 (예: `Map/TileData`, `Map/MapGenerateData`, `Core/Event/Event Channel`).
- 무거운 계산은 `Task.Run`으로 백그라운드 처리 후 메인 스레드에서 Unity API 반영 (예: `MapGenerator` — 데이터만 계산하고 `Flush()`로 Tilemap에 일괄 적용). 백그라운드 스레드에서 Unity 오브젝트 API를 호출하지 않는다.
- 타일맵 변경은 즉시 반영하지 않고 변경 스택에 쌓았다가 `IFlush.Flush()`로 일괄 `SetTiles`.
- 타일 좌표는 `Vector3Int`/`Vector2Int`, 월드 좌표 변환은 각 시스템의 `WorldToTile`/`TileToWorld`(또는 `Grid.WorldToCell`)를 사용.
