using Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Tablet
{
    public class TabletCanvas : MonoBehaviour
    {
        [SerializeField]
        private Text _taskTitle;
        private TasksCollection _tasksCollection;
        
        public void Start()
        {
            UpdateText();
            _tasksCollection.Notify += UpdateText;
        }
        

        [Inject]
        public void Construct(TasksCollection tasksCollection)
        {
            _tasksCollection = tasksCollection;
        }

        private void UpdateText()
        {
            _taskTitle.text = _tasksCollection.CurrentTask().Title;
        }
    }
}