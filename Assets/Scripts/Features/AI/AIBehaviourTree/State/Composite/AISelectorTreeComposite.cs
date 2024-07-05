using Features.AI.AIStateMachine.Conditions;

namespace Features.AI.AIStateMachine.Composite
{
    public class AISelectorTreeComposite : AITreeComposite
    {
        public AICondition[] Conditions;
        
        public override bool CanEnterInComposite()
        {
            return true;
        }

        public override void TrySetState()
        {
            foreach (var condition in Conditions)
            {
                if (condition.IsSuccess())
                {
                    condition.Composite.TrySetState();
                    break;
                }
            }
        }

        public override void OnEnter()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}