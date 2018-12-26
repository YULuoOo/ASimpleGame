using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDefines{
    public const string startScene = "StartGame";
    public const string Create = "CreateCharacter";
    public const string mainScene = "Scene_Forest";

    public const string playerName = "playerName";
    public const string playerRole = "playerRole";

    public const string warriorPath = "Prefabs/Character/Warrior";
    public const string archerPath = "Prefabs/Character/Archer";
    public const string enermyPath = "Prefabs/Character/Enermy";

    public const string animIdle = "Idle";
    public const string animRun = "Run";
    public const string animDeath = "Death";
    public const string animHit = "Hit";
    public const string animAttack1 = "Attack1";
    public const string animAttack2 = "Attack2";

    public const string warriorSkill1 = "Prefabs/FX/WarriorSkill1";
    public const string warriorSkill2 = "Prefabs/FX/WarriorSkill2";

    public const string archerSkill1 = "Prefabs/FX/ArcherSkill1";
    public const string archerSkill2 = "Prefabs/FX/ArcherSkill2";
    public const string archerSkillHit = "Prefabs/FX/ArrowHit";

    public const string UISlider = "Prefabs/UI/HPSlider";
    public const string UIDamageNum = "Prefabs/UI/DamageNum";
    public const string UIGameOver = "Prefabs/UI/GameOverPanel";


    public const int skillFXForeverTime = -100;
}
public enum CharacterType
{
    warrior = 1,
    archer = 2,
}
