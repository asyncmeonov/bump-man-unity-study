using UnityEngine;

[CreateAssetMenu(fileName = "mob_newMobDef", menuName = "Mob/New Mob Asset Definition")]
public class MobAssetDefinition : ScriptableObject
{
    public Sprite horizontalSprite;

    public Sprite upSprite;

    public Sprite downSprite;

    public Sprite afraidSprite;

    public AudioEvent walkingSfx;

}
