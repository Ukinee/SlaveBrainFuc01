using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Structures.Views.Interfaces;
using UnityEngine;

namespace Codebase.Structures.Views.Implementations
{
    public class StructureView : MonoBehaviour, IStructureView
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

        private void LateUpdate()
        {
            Vector3 angles = transform.rotation.eulerAngles;
            
            transform.position = new Vector3(transform.position.x, GameConstants.YOffset, transform.position.z);
            transform.rotation = Quaternion.Euler(new Vector3(0, angles.y, 0));//todo: huinya
        }
        
        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
