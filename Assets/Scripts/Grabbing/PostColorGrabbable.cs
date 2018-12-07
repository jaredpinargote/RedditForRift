using UnityEngine;
public class PostColorGrabbable : OVRGrabbable
{
    public static readonly Color COLOR_GRAB = new Color(1.0f, 0.5f, 0.0f, 1.0f);
    public static readonly Color COLOR_HIGHLIGHT = new Color(1.0f, 0.0f, 1.0f, 1.0f);

    private Color m_color = Color.black;
    private CanvasRenderer[] m_canvasRenderers = null;
    private bool m_highlight;
    
    public bool Highlight
    {
        get { return m_highlight; }
        set
        {
            m_highlight = value;
            UpdateColor();
        }
    }

    protected void UpdateColor()
    {
        if (isGrabbed) SetColor(COLOR_GRAB);
        else if (Highlight) SetColor(COLOR_HIGHLIGHT);
        else SetColor(m_color);

    }

    override public void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        UpdateColor();
    }

    override public void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        UpdateColor();
    }

    void Awake()
    {
        if (m_grabPoints.Length == 0)
        {
            // Get the collider from the grabbable
            Collider collider = this.GetComponent<Collider>();
            if (collider == null)
            {
                throw new System.ArgumentException("Grabbables cannot have zero grab points and no collider -- please add a grab point or collider.");
            }

            // Create a default grab point
            m_grabPoints = new Collider[1] { collider };

            // Grab points are doing double-duty as a way to identify submeshes that should be colored.
            // If unspecified, just color self.
            m_canvasRenderers = new CanvasRenderer[1];
            m_canvasRenderers[0] = this.GetComponent<CanvasRenderer>();
        }
        else
        {
            m_canvasRenderers = this.GetComponentsInChildren<CanvasRenderer>();
        }
        m_color = new Color(
            Random.Range(0.1f, 0.95f),
            Random.Range(0.1f, 0.95f),
            Random.Range(0.1f, 0.95f),
            1.0f
        );
        SetColor(m_color);
    }

    private void SetColor(Color color)
    {
        for (int i = 0; i < m_canvasRenderers.Length; ++i)
        {
            CanvasRenderer canvasRenderer = m_canvasRenderers[i];
            canvasRenderer.SetColor(color);
            // for (int j = 0; j < canvasRenderer.materials.Length; ++j)
            // {
            //     Material meshMaterial = canvasRenderer.materials[j];
            //     meshMaterial.color = color;
            // }
        }
    }
}