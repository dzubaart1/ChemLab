using System.Collections.Generic;
using System.Linq;
using BNG;
using Generators;
using Substances;
using Tasks;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class FirstLabInstaller : MonoInstaller
    {
        public GameObject oculusRig;
        public override void InstallBindings()
        {
            GameObject oculusRigInst = Container.InstantiatePrefab(oculusRig);

            List<Grabber> grabbers = oculusRigInst.GetComponentsInChildren<Grabber>().ToList();
            Container.Bind<List<Grabber>>().FromInstance(grabbers).AsSingle();
            
            SubstancesParamsCollection substancesCollection = new SubstancesParamsCollection();
            Container.Bind<SubstancesParamsCollection>().FromInstance(substancesCollection).AsSingle();
            
            TasksCollection tasksCollection = new TasksCollection();
            Container.Bind<TasksCollection>().FromInstance(tasksCollection).AsSingle();

            IdGenerator idGenerator = new IdGenerator();
            Container.Bind<IdGenerator>().FromInstance(idGenerator).AsSingle();
        }
    }
}
