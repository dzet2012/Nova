using UnityEngine;

namespace Nova
{
    [ExportCustomType]
    public class MaterialPoolComponent : MonoBehaviour
    {
        // Keep Renderer's default material, used when turning off VFX on the Renderer
        // DefaultMaterial is null for CameraController
        private Material _defaultMaterial;

        public Material defaultMaterial
        {
            get => _defaultMaterial;
            set
            {
                Utils.DestroyMaterial(_defaultMaterial);
                _defaultMaterial = value;
            }
        }

        private void Awake()
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                defaultMaterial = renderer.material;
            }
        }

        private void OnDestroy()
        {
            defaultMaterial = null;
        }

        private readonly MaterialFactory _factory = new MaterialFactory();

        public MaterialFactory factory => _factory;

        public Material Get(string shaderName)
        {
            return _factory.Get(shaderName);
        }

        public RestorableMaterial GetRestorableMaterial(string shaderName)
        {
            return _factory.GetRestorableMaterial(shaderName);
        }

        public static MaterialPoolComponent Ensure(GameObject gameObject)
        {
            var pool = gameObject.GetComponent<MaterialPoolComponent>();
            if (pool == null)
            {
                pool = gameObject.AddComponent<MaterialPoolComponent>();
            }

            return pool;
        }
    }
}