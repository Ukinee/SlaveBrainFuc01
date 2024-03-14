using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
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

        public void Collide(Vector3 direction, Vector3 position)
        {
            Rigidbody.AddForceAtPosition(-direction * 5, position, ForceMode.Impulse); //todo: Hardcoded Values
        }
        
        public void Dispose()
        {
            if(gameObject.transform.childCount != 0)
                "Structure has children on dispose".Log();
            
         //   Destroy(gameObject);
        }
    }
}
