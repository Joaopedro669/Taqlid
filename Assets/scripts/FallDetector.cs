using UnityEngine;

public class FallDetector : MonoBehaviour
{
    // Cor da linha que vai aparecer apenas no editor da Unity
    [SerializeField] private Color debugColor = Color.red;

    private void OnDrawGizmos()
    {
        // Desenha uma caixa vermelha no Editor para você ver o tamanho do abismo
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box != null)
        {
            Gizmos.color = debugColor;
            Vector3 size = new Vector3(box.size.x * transform.localScale.x, box.size.y * transform.localScale.y, 1);
            Gizmos.DrawWireCube(transform.position + (Vector3)box.offset, size);
        }
    }
}

