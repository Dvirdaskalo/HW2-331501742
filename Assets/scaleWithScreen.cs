using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class scaleWithScreen2 : MonoBehaviour
{
   

    
    
        void Update()
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            float height = Camera.main.orthographicSize * 2f;
            float width = 5+height * Screen.width / Screen.height;

            sr.size = new Vector2(width,sr.size.y);
        }
    
}
