using _Script._Map._MapGenerator;

namespace _Script._Map._MapLayer._Layer._Layers._BiomeLayer
{
    public class BiomeLayer : AbstractMapLayer
    {
        public override LayerType GetLayerType() => LayerType.Biome;


        public BiomeData CreateBiome(MapGenerateData mapGenerateData)
        {
            BiomeData biomeData = new BiomeData();


            
            
            return biomeData;
        }
        
        
    }
}