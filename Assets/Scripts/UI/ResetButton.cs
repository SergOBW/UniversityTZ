using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResetButton : MonoBehaviour
    {
        [SerializeField]private Transform jawModel;
        [SerializeField]private Transform teethMainTransform;
        [SerializeField]private Transform initialTeethParent;
    
        private Transform[] _teeth;

        private Vector3 _initialJawPosition;
        private Quaternion _initialJawRotation;
        private Vector3[] _initialTeethPositions;
    
        private void Awake()
        {
            Transform[] allChildren = teethMainTransform.GetComponentsInChildren<Transform>();
            _teeth = allChildren.Where(t => t != teethMainTransform).ToArray();
            if (_teeth.Length <= 0)
            {
                Debug.LogError("There is no teeth found! Check please.");
            }
        
        }


        void Start()
        {
            _initialJawPosition = jawModel.position;
            _initialJawRotation = jawModel.rotation;

            _initialTeethPositions = new Vector3[_teeth.Length];
            for (int i = 0; i < _teeth.Length; i++)
            {
                _initialTeethPositions[i] = _teeth[i].position;
            }

            Button btn = GetComponent<Button>();
            btn.onClick.AddListener(ResetAll);
        }

        void ResetAll()
        {
            jawModel.position = Vector3.zero;
            jawModel.rotation = Quaternion.identity;

            for (int i = 0; i < _teeth.Length; i++)
            {
                _teeth[i].position = Vector3.zero;
                _teeth[i].parent = initialTeethParent;
                _teeth[i].localRotation = Quaternion.identity;
            }
        }
    }
}
