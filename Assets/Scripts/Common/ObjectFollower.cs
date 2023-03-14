using UnityEngine;

namespace Snowlers.Common
{
    public class ObjectFollower : MonoBehaviour
    {
        [SerializeField] Transform m_object;
        [SerializeField] bool m_freezeX = false;
        [SerializeField] bool m_freezeY = false;
        [SerializeField] bool m_useSceneOffset = false;

        private Vector3 m_offset;
        
        public void SetObject(Transform obj)
        {
            m_object = obj;
        }

        private void Start()
        {
            m_offset = transform.position - m_object.position;
        }

        private void Update()
        {
            Vector3 newPosition = m_object.position;

            if (m_freezeX)
                newPosition.x = transform.position.x;

            if (m_freezeY)
                newPosition.y = transform.position.y;

            if (m_useSceneOffset)
                newPosition += m_offset;

            transform.position = newPosition;
        }
    }
}
