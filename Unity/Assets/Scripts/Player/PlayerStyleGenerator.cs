using UnityEngine;
using System.Collections;

public class PlayerStyleGenerator : MonoBehaviour
{


    public Sprite[] Hairs;
    public Sprite[] Heads;
    public Sprite[] Noses;
    public Sprite[] Numbers;


    public GameObject TargetPlayer;
    protected Transform GraphicsTransform;
    protected Transform HeadTransform;
    protected Transform NoseTransform;
    protected Transform HairTransform;

    protected Transform CanvasTransform;
    protected Transform KeyTransform;
    protected int Index;
    // Use this for initialization
    void Start()
    {


    }

    private void GenerateStyle()
    {
        GetTransforms();

        var hairIndex = Random.Range(Hairs.GetLowerBound(0), Hairs.GetUpperBound(0));
        var headIndex = Random.Range(Heads.GetLowerBound(0), Heads.GetUpperBound(0));
        var noseIndex = Random.Range(Noses.GetLowerBound(0), Noses.GetUpperBound(0));

        HairTransform.GetComponent<SpriteRenderer>().sprite = Hairs[hairIndex];
        HeadTransform.GetComponent<SpriteRenderer>().sprite = Heads[headIndex];
        NoseTransform.GetComponent<SpriteRenderer>().sprite = Noses[noseIndex];

        KeyTransform.GetComponent<UnityEngine.UI.Image>().sprite = Numbers[Index];
    }

    private void GetTransforms()
    {
        this.CanvasTransform = TargetPlayer.transform.FindChild("Canvas");
        this.KeyTransform = CanvasTransform.transform.FindChild("Key");
        this.GraphicsTransform = TargetPlayer.transform.FindChild("Graphics");
        this.HeadTransform = GraphicsTransform.FindChild("Head");
        this.NoseTransform = HeadTransform.FindChild("Nose");
        this.HairTransform = HeadTransform.FindChild("Hair");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayerInstantiated(PlayerSpawner.PlayerInstantiatedData playerData)
    {
        this.TargetPlayer = playerData.GameObject;
        this.Index = playerData.index;
        GenerateStyle();
    }
}
