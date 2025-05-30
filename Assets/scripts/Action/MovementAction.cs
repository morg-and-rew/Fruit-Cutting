using UnityEngine;

namespace FruitCutting.CharactersActions
{
    public class MovementAction : MonoBehaviour
    {
        public bool CanMove { get; set; } = true;

        public void FixedUpdateMove(Transform moveObj, Vector3 diraction, float speed)
        {
            if (!CanMove)
                return;

            moveObj.transform.position += diraction * (speed * Time.deltaTime);
        }

        public void UpdateMove(Transform moveObj, Vector3 diraction, float speed)
        {
            if (!CanMove)
                return;

            moveObj.transform.position += diraction * (speed * Time.deltaTime);
        }
    }
}
