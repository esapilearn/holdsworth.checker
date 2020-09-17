﻿using ESAPIX.Common;
using ESAPIX.Constraints.DVH;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Holdsworth.Checker.ViewModels
{
    public class MainViewModel : BindableBase
    {
        AppComThread VMS = AppComThread.Instance;

        public MainViewModel()
        {
            EvaluateCommand = new DelegateCommand(() =>
            {
                foreach (var pc in Constraints)
                {
                    var result = VMS.GetValue(sc =>
                    {
                        //Check if we can constrain first
                        var canConstrain = pc.Constraint.CanConstrain(sc.PlanSetup);
                        //If not..report why
                        if (!canConstrain.IsSuccess) { return canConstrain; }
                        else
                        {
                            //Can constrain - so do it
                            return pc.Constraint.Constrain(sc.PlanSetup);
                        }
                    });
                    //Update UI
                    pc.Result = result;
                }
            });

            CreateConstraints();
        }

        private void CreateConstraints()
        {
            Constraints.AddRange(new PlanConstraint[]
            {
                new PlanConstraint(ConstraintBuilder.Build("PTV", "Max[%] <= 110")),
                new PlanConstraint(ConstraintBuilder.Build("Rectum", "V75Gy[%] <= 15")),
                new PlanConstraint(ConstraintBuilder.Build("Rectum", "V65Gy[%] <= 35")),
                new PlanConstraint(ConstraintBuilder.Build("Bladder", "V80Gy[%] <= 15")),
               // new PlanConstraint(new CTDateConstraint())
            });
        }


        public DelegateCommand EvaluateCommand { get; set; }
        public ObservableCollection<PlanConstraint> Constraints { get; private set; } = new ObservableCollection<PlanConstraint>();
    }

}
