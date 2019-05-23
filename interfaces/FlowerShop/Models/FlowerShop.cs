using System;
using System.Collections.Generic;

namespace interfaces
{
class FlowerShop{
    public List<Rose> createRoseBouqet(){
        return new List<Rose>(){
            new Rose(){
                color = "red",
                species = "smellyjelly",
                stemLength = 6,
                thorned = false,
                smellsGood = false,
            },
            new Rose(){
                color = "purple",
                species = "purplepeopleeater",
                stemLength = 5,
                thorned = true,
                smellsGood = true,
            }
        };
    }
  public List<IWeddingFlower> createWeddingBouqet(){
        return new List<IWeddingFlower>(){
             new Lily(){
                name = "Lily",
                color = "White",
                species = "sillylily",
                stemLength = 5,
                smellsGood = true,
            },
              new Lily(){
                name = "Lily",
                color = "Yellow",
                species = "daffy duck",
                stemLength = 4,
                smellsGood = false,
            }

        };
}
    public List<IMothersDayFlower> createMothersDayBouqet(){
        return new List<IMothersDayFlower>(){
             new Lily(){
                name = "Lily",
                color = "purple",
                species = "lilyoftheeast",
                stemLength = 5,
                smellsGood = true,
            },
              new Lily(){
                name = "Lily",
                color = "Yellow",
                species = "daffy duck",
                stemLength = 4,
                smellsGood = false,
            }

        };
}
}
}