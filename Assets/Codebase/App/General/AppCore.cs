using Codebase.App.Infrastructure.StateMachines;
using UnityEngine;

namespace Codebase.App.General
{
    public class AppCore : MonoBehaviour
    {
        private SceneStateMachineService _stateMachineServiceService;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Construct(SceneStateMachineService stateMachineServiceService)
        {
            _stateMachineServiceService = stateMachineServiceService;
        }

        private void Update()
        {
            _stateMachineServiceService?.Update(Time.deltaTime);
        }

        private void LateUpdate()
        {
            _stateMachineServiceService?.LateUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _stateMachineServiceService?.FixedUpdate(Time.fixedDeltaTime);
        }

        public void Init()
        {
            _stateMachineServiceService.Init();
        }
    }
}
