using UnityEngine;
[ExecuteAlways]
public class RandomizeAurora : MonoBehaviour
{
    public Shader DefaultShader;
    [Header("Vertex Offset")]
    public ParticleSystem.MinMaxCurve VertexOffsetStrength = new ParticleSystem.MinMaxCurve();
    [Space]
    public ParticleSystem.MinMaxCurve VertexOffsetSpeedX = new ParticleSystem.MinMaxCurve();
    public ParticleSystem.MinMaxCurve VertexOffsetSpeedY = new ParticleSystem.MinMaxCurve();
    [Space]
    public ParticleSystem.MinMaxCurve VertexOffsetTileX = new ParticleSystem.MinMaxCurve();
    public ParticleSystem.MinMaxCurve VertexOffsetTileY = new ParticleSystem.MinMaxCurve();
    [Space]
    [Header("Indentation")]
    public ParticleSystem.MinMaxCurve IndentationStrength = new ParticleSystem.MinMaxCurve();
    [Space]
    public ParticleSystem.MinMaxCurve IndentationSpeedX = new ParticleSystem.MinMaxCurve();
    public ParticleSystem.MinMaxCurve IndentationSpeedY = new ParticleSystem.MinMaxCurve();
    [Space]
    public ParticleSystem.MinMaxCurve IndentationTileX = new ParticleSystem.MinMaxCurve();
    public ParticleSystem.MinMaxCurve IndentationTileY = new ParticleSystem.MinMaxCurve();
    [Space]
    [Header("Lines")]
    public ParticleSystem.MinMaxCurve LinesStrength = new ParticleSystem.MinMaxCurve();
    [Space]
    public ParticleSystem.MinMaxCurve LinesSpeedX = new ParticleSystem.MinMaxCurve();
    public ParticleSystem.MinMaxCurve LinesSpeedY = new ParticleSystem.MinMaxCurve();
    [Space]
    public ParticleSystem.MinMaxCurve LinesTileX = new ParticleSystem.MinMaxCurve();
    public ParticleSystem.MinMaxCurve LinesTileY = new ParticleSystem.MinMaxCurve();
    [Space]
    [Header("Colour")]
    public ParticleSystem.MinMaxCurve ColourSlider = new ParticleSystem.MinMaxCurve();
    [Space]
    public ParticleSystem.MinMaxGradient Colour1 = new ParticleSystem.MinMaxGradient();
    public ParticleSystem.MinMaxGradient Colour2 = new ParticleSystem.MinMaxGradient();
    [Space]
    [Header("Misc")]
    public ParticleSystem.MinMaxCurve EmissiveStrength = new ParticleSystem.MinMaxCurve();

    private Vector2 VertexOffsetSpeed => new Vector2(Evaluate(VertexOffsetSpeedX), Evaluate(VertexOffsetSpeedY));
    private Vector2 VertexOffsetTile => new Vector2(Evaluate(VertexOffsetTileX), Evaluate(VertexOffsetTileY));

    private Vector2 IndentationSpeed => new Vector2(Evaluate(IndentationSpeedX), Evaluate(IndentationSpeedY));
    private Vector2 IndentationTile => new Vector2(Evaluate(IndentationTileX), Evaluate(IndentationTileY));

    private Vector2 LinesSpeed => new Vector2(Evaluate(LinesSpeedX), Evaluate(LinesSpeedY));
    private Vector2 LinesTile => new Vector2(Evaluate(LinesTileX), Evaluate(LinesTileY));

    private Renderer Renderer;
    private Material Material;

    //Value names
    private const string Strength = "Strength";
    private const string Speed = "Speed";
    private const string Tile = "Tile";

    //Commonly used Prefix names
    private const string VO = "_VO_";
    private const string Indentation = "_Indentation_";
    private const string Lines = "_Lines_";
    private const string Colour = "_Color_";

    public void OnEnable()
    {
    }

    public void OnValidate()
    {
        AssignValues();
        Material.SetFloat(VO + Strength, Evaluate(VertexOffsetStrength));
        Material.SetVector(VO + Speed, VertexOffsetSpeed);
        Material.SetVector(VO + Tile, VertexOffsetTile);

        Material.SetFloat(Indentation + Strength, Evaluate(IndentationStrength));
        Material.SetVector(Indentation + Speed, IndentationSpeed);
        Material.SetVector(Indentation + Tile, IndentationTile);

        Material.SetFloat(Lines + Strength, Evaluate(LinesStrength));
        Material.SetVector(Lines + Speed, LinesSpeed);
        Material.SetVector(Lines + Tile, LinesTile);

        Material.SetFloat(Colour + "Slider", Evaluate(ColourSlider));
        Material.SetColor(Colour + 1, Evaluate(Colour1));
        Material.SetColor(Colour + 2, Evaluate(Colour2));

        Material.SetFloat("_Emissive_" + Strength, Evaluate(EmissiveStrength));

        Renderer.sharedMaterial = Material;
    }

    public void Reset()
    {
        AssignValues();
    }

    private void AssignValues()
    {
        if (Renderer == null)
            Renderer = GetComponentInChildren<Renderer>();
#if UNITY_EDITOR
        Material = Renderer.sharedMaterial;
        if (Material == null)
        {
            Material NewMat = new Material(DefaultShader)
            {
                doubleSidedGI = true
            };
            Material = NewMat;

        }
        else
        Material = new Material(Material);
#else
        Material = Renderer.material;
#endif
        //        Renderer.sharedMaterial = Material;
    }

    private float Evaluate(ParticleSystem.MinMaxCurve curve)
    {
        return curve.Evaluate(Time.time,Random.value);
    }
    private Color Evaluate(ParticleSystem.MinMaxGradient gradient)
    {
        return gradient.Evaluate(Time.time,Random.value);
    }
}
