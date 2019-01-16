using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class Everjust : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Everjust");
            Tooltip.SetDefault(@"y'all everjust true melee nice guy
i can't believe someone took the time to actually sprite this
what did dawn say at the end of that one video 
oh right,  yeah,  
where is my dev set
right there was also something about the ocean and being eternal
idrc tho
by the way if dawn sees this don't worry about the ancient manipulator thing you can use it for nice guy
anyway yeah enjoy this dawn 
i reward your efforts with more efforts muahahahaha 
also rip dawn's sanity after nohitting angel trap 
oh one last thing
this weapon can crash your game if you favorite it so FOR THE LOVE OF GOD DON'T DO THAT AAAAAA");
        }
        public override void SetDefaults()
        {
            item.damage = 52;
            item.melee = true;
            item.useStyle = 1;
            item.knockBack = 3;
            item.useTime = 20;
            item.useAnimation = 20;
            item.width = 350;
            item.height = 350;
            item.value = 8000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.maxStack = 1;
            item.autoReuse = true;
            item.useTurn = true;
        }
	}
}
