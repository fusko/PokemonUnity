﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonUnity;
using PokemonUnity.Combat;
using PokemonUnity.Inventory;


namespace PokemonUnity
{
	public interface ISceneState
	{
		void updatemini();
	}
	public interface IScreen
	{
		void update();
	}
	public interface IGame_Screen
	{
		int brightness { get; }
		int flash_color { get; }
		int pictures { get; }
		int shake { get; }
		int tone { get; }
		int weather_max { get; }
		Overworld.FieldWeathers weather_type { get; }

		void start_flash(Color color, int duration);
		void start_shake(float power, float speed, int duration);
		//void start_tone_change(Tone tone, int duration);
		void update();
		void weather(int type, int power, int duration);
	}
	public interface IWindow
	{
	}
	public interface ICanvas
	{
	}

	#region Audio
	public interface IAudioObject
	{
		string name { get; }
		float volume { get; }
		float pitch { get; }
		int position { get; }
		/// <summary>
		/// length/duration
		/// </summary>
		int time { get; }
		IAudioObject clone();
		void play();
	}
	#endregion

	#region Relic Stone
	/// <summary>
	/// Scene class for handling appearance of the screen
	/// </summary>
	public interface IRelicStoneScene : IScene
	{
		void initialize(IScene scene);
		void pbUpdate();
		void pbRefresh();
		void pbPurify();
		//void pbConfirm(string msg);
		void pbDisplay(string msg, bool brief = false);
		void pbStartScene(Monster.Pokemon pokemon);
		void pbEndScene();
	}
	/// <summary>
	/// Screen class for handling game logic
	/// </summary>
	public interface IRelicStoneScreen : IScreen
	{
		void initialize(IRelicStoneScene scene);
		void pbRefresh();
		void pbConfirm(string x);
		void pbDisplay(string x);
		void pbStartScreen(Monster.Pokemon pokemon);
	}
	#endregion

	#region Text Entry
	public interface IPokemonEntry : IScreen
	{
		void initialize(IScene scene);
		string pbStartScreen(string helptext, int minlength, int maxlength, string initialText, int mode = -1, Monster.Pokemon pokemon = null);
	}

	/// <summary>
	/// Text entry screen - free typing.
	/// </summary>
	public interface IPokemonEntryScene : IScene
	{
		void pbStartScene(string helptext, int minlength, int maxlength, string initialText, int subject = 0, Monster.Pokemon pokemon = null);
		void pbEndScene();
		string pbEntry();
		//string pbEntry1();
		//string pbEntry2();
	}

	/// <summary>
	/// Text entry screen - arrows to select letter.
	/// </summary>
	public interface IPokemonEntryScene2 : IScene, IPokemonEntryScene
	{
		//void pbStartScene(string helptext, int minlength, int maxlength, string initialText, int subject = 0, Pokemon pokemon = null);
		//void pbEndScene();
		//string pbEntry();
		void pbUpdate();
		void pbChangeTab(int newtab = 0);
		bool pbColumnEmpty(int m);
		void pbUpdateOverlay();
		void pbDoUpdateOverlay();
		void pbDoUpdateOverlay2();
		bool pbMoveCursor();
		float wrapmod(float x, float y);
	}
	#endregion

	#region Pokemon Battle
	public interface IPokeBattle_Scene : IScene
	{
		/*
		-  def pbChooseNewEnemy(int index,party)
		Use this method to choose a new Pokémon for the enemy
		The enemy's party is guaranteed to have at least one 
		choosable member.
		index - Index to the battler to be replaced (use e.g. @battle.battlers[index] to 
		access the battler)
		party - Enemy's party

		- def pbWildBattleSuccess
		This method is called when the player wins a wild Pokémon battle.
		This method can change the battle's music for example.

		- def pbTrainerBattleSuccess
		This method is called when the player wins a Trainer battle.
		This method can change the battle's music for example.

		- def pbFainted(pkmn)
		This method is called whenever a Pokémon faints.
		pkmn - PokeBattle_Battler object indicating the Pokémon that fainted

		- def pbChooseEnemyCommand(int index)
		Use this method to choose a command for the enemy.
		index - Index of enemy battler (use e.g. @battle.battlers[index] to 
		access the battler)
		*/

		void initialize();
		void pbUpdate();
		void pbGraphicsUpdate();
		void pbInputUpdate();
		void pbShowWindow(int windowtype);
		void pbSetMessageMode(int mode);
		void pbWaitMessage();
		void pbDisplay(string msg, bool brief = false);
		void pbDisplayMessage(string msg, bool brief = false);
		void pbDisplayPausedMessage(string msg);
		bool pbDisplayConfirmMessage(string msg);
		void pbShowCommands(string msg, string commands, bool defaultValue);
		void pbFrameUpdate(object cw = null);
		void pbRefresh();
		void pbAddSprite(int id, double x, double y, string filename, int viewport);
		void pbAddPlane(int id, string filename, int viewport);
		void pbDisposeSprites();
		void pbBeginCommandPhase();
		void pbShowOpponent(int index);
		void pbHideOpponent();
		void pbShowHelp(int text);
		void pbHideHelp();
		void pbBackdrop();
		/// <summary>
		/// Returns whether the party line-ups are currently appearing on-screen
		/// </summary>
		/// <returns></returns>
		bool inPartyAnimation();
		/// <summary>
		/// Shows the party line-ups appearing on-screen
		/// </summary>
		void partyAnimationUpdate();
		void pbStartBattle(PokemonUnity.Combat.Battle battle);
		void pbEndBattle(BattleResults result);
		void pbRecall(int battlerindex);
		void pbTrainerSendOut(int battlerindex, Combat.Pokemon pkmn);
		/// <summary>
		/// Player sending out Pokémon
		/// </summary>
		/// <param name="battlerindex"></param>
		/// <param name="pkmn"></param>
		void pbSendOut(int battlerindex, Combat.Pokemon pkmn);
		void pbTrainerWithdraw(Combat.Battle battle, Combat.Pokemon pkmn);
		void pbWithdraw(Combat.Battle battle, Combat.Pokemon pkmn);
		void pbMoveString(string move);
		void pbBeginAttackPhase();
		void pbSafariStart();
		void pbResetCommandIndices();
		void pbResetMoveIndex(int index);
		int pbSafariCommandMenu(int index);
		/// <summary>
		/// Use this method to display the list of commands and choose
		/// a command for the player.
		/// </summary>
		/// 0 - Fight, 1 - Pokémon, 2 - Bag, 3 - Run
		/// <param name="index">Index of battler (use e.g. @battle.battlers[index] to 
		/// access the battler)</param>
		/// <returns> Return values: 0=Fight, 1=Bag, 2=Pokémon, 3=Run, 4=Call</returns>
		int pbCommandMenu(int index);
		/// <summary>
		/// </summary>
		/// <param name="index"></param>
		/// <param name="texts"></param>
		/// <param name="mode">0 - regular battle, 1 - Shadow Pokémon battle, 2 - Safari Zone, 3 - Bug Catching Contest</param>
		int pbCommandMenuEx(int index, string texts, int mode = 0);
		/// <summary>
		/// Update selected command
		/// Use this method to display the list of moves for a Pokémon
		/// </summary>
		/// <param name="index"></param>
		int pbFightMenu(int index);
		/// <summary>
		/// Use this method to display the inventory
		/// The return value is the item chosen, or 0 if the choice was canceled.
		/// </summary>
		/// <param name="index"></param>
		int[] pbItemMenu(int index);
		/// <summary>
		/// Called whenever a Pokémon should forget a move.  It should return -1 if the
		/// selection is canceled, or 0 to 3 to indicate the move to forget.  The function
		/// should not allow HM moves to be forgotten.
		/// </summary>
		/// <param name="pokemon"></param>
		/// <param name="moveToLearn"></param>
		int pbForgetMove(Pokemon pokemon, Moves moveToLearn);
		/// <summary>
		/// Called whenever a Pokémon needs one of its moves chosen. Used for Ether.
		/// </summary>
		/// <param name=""></param>
		/// <param name="message"></param>
		int pbChooseMove(Monster.Pokemon pokemon, string message);
		string pbNameEntry(string helptext, Monster.Pokemon pokemon);
		void pbSelectBattler(int index, int selectmode = 1);
		void pbFirstTarget(int index, int targettype);
		void pbUpdateSelected(int index);
		/// <summary>
		/// Use this method to make the player choose a target 
		/// for certain moves in double battles.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="targettype"></param>
		void pbChooseTarget(int index, int targettype);
		int pbSwitch(int index, bool lax, bool cancancel);
		void pbDamageAnimation(Combat.Pokemon pkmn, float effectiveness);
		/// <summary>
		/// This method is called whenever a Pokémon's HP changes.
		/// Used to animate the HP bar.
		/// </summary>
		/// <param name="pkmn"></param>
		/// <param name="oldhp"></param>
		/// <param name="anim"></param>
		void pbHPChanged(int pkmn, int oldhp, bool anim = false);
		/// <summary>
		/// This method is called whenever a Pokémon faints.
		/// </summary>
		/// <param name=""></param>
		void Fainted(int pkmn);
		void pbFainted(int pkmn);
		/// <summary>
		/// Use this method to choose a command for the enemy.
		/// </summary>
		/// <param name="index"></param>
		void pbChooseEnemyCommand(int index);
		/// <summary>
		/// Use this method to choose a new Pokémon for the enemy
		/// The enemy's party is guaranteed to have at least one choosable member.
		/// </summary>
		/// <param name="index"></param>
		/// <param name=""></param>
		int pbChooseNewEnemy(int index, Pokemon[] party);
		/// <summary>
		/// This method is called when the player wins a wild Pokémon battle.
		/// This method can change the battle's music for example.
		/// </summary>
		void pbWildBattleSuccess();
		/// <summary>
		/// This method is called when the player wins a Trainer battle.
		/// This method can change the battle's music for example.
		/// </summary>
		void pbTrainerBattleSuccess();
		void pbEXPBar(Pokemon pokemon, Pokemon battler, int startexp, int endexp, int tempexp1, int tempexp2);
		void pbShowPokedex(Pokemons species, int form = 0);
		void pbChangeSpecies(Pokemon attacker, Pokemons species);
		void ChangePokemon();
		void pbChangePokemon(Pokemon attacker, Monster.Forms pokemon);
		void pbSaveShadows();
		void pbFindAnimation(Moves moveid, int userIndex, int hitnum);
		void pbCommonAnimation(string animname, Combat.Pokemon user, Combat.Pokemon target, int hitnum = 0);
		void pbAnimation(Moves moveid, Combat.Pokemon user, Combat.Pokemon target, int hitnum = 0);
		void pbAnimationCore(string animation, Combat.Pokemon user, Combat.Pokemon target, bool oppmove = false);
		void pbLevelUp(Pokemon pokemon, Pokemon battler, int oldtotalhp, int oldattack, int olddefense, int oldspeed, int oldspatk, int oldspdef);
		void pbThrowAndDeflect(Items ball, int targetBattler);
		void pbThrow(Items ball, int shakes, bool critical, int targetBattler, bool showplayer = false);
		void pbThrowSuccess();
		void pbHideCaptureBall();
		void pbThrowBait();
		void pbThrowRock();
		void HPChanged(int index, int oldhp, bool animate = false);
		void pbHPChanged(Pokemon pkmn, int oldhp, bool animate = false);
	}
	#endregion

	#region Evolution
	public interface IPokemonEvolutionScene
	{
		void pbEndScreen();
		void pbEvolution(bool cancancel = true);
		//void pbFlashInOut(bool canceled, oldstate, oldstate2);
		void pbStartScreen(Monster.Pokemon pokemon, Pokemons newspecies);
		void pbUpdate(bool animating = false);
		void pbUpdateExpandScreen();
		void pbUpdateNarrowScreen();
	}
	#endregion

	#region Pokedex
	public interface IPokemonPokedex : IScreen
	{
		void initialize(IPokemonPokedexScene scene);
		void pbDexEntry(Pokemons species);
		void pbStartScreen();
	}
	public interface IPokemonPokedexScene : IScene
	{
		void pbUpdate();
		void pbEndScene();
		void pbStartScene();
		void pbStartDexEntryScene(Pokemons species);
		void pbPokedex();
		void pbDexEntry(int index);
		int pbDexSearch();
		void pbCloseSearch();
		IEnumerable<Monster.Data.PokemonData> pbSearchDexList(params object[] param);
		List<Monster.Data.PokemonData> pbGetDexList();
		void pbRefreshDexList(int index = 0);
		void pbRefreshDexSearch(params string[] param);
		bool pbCanAddForModeList(int mode, Pokemons nationalSpecies);
		bool pbCanAddForModeSearch(int mode, Pokemons nationalSpecies);
		void pbChangeToDexEntry(Pokemons species);
		int pbDexSearchCommands(string[] commands, int selitem, string[] helptexts = null);
		int pbGetPokedexRegion();
		int pbGetSavePositionIndex();
		void pbMiddleDexEntryScene();
		void setIconBitmap(Pokemons species);
	}
	#endregion

	#region Pause Menu
	public interface IPokemonMenu_Scene
	{
		void pbEndScene();
		void pbHideMenu();
		void pbRefresh();
		void pbShowCommands(string[] commands);
		void pbShowHelp(string text);
		void pbShowInfo(string text);
		void pbShowMenu();
		void pbStartScene();
	}

	public interface IPokemonMenu
	{
		void initialize(IPokemonMenu_Scene scene);
		void pbShowMenu();
		void pbStartPokemonMenu();
	}
	#endregion

	#region 
	#endregion

	namespace UX
	{
		public interface IFrontEnd
		{
		}
	}
}