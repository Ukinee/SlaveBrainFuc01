

using UnityEngine;

namespace Codebase.App.General
{
    [DefaultExecutionOrder(-1)]
    public class Bootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            AppCore appCore = FindObjectOfType<AppCore>() ?? new AppCoreBuilder().Build();
            //intendedException;
        }
    }
}
