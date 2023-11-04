namespace AISystem
{
    public abstract class AIState
    {
        protected AIItem _aiItem;

        public AIItem AIItem { set => _aiItem = value; }

        public abstract void ActiveState(AIController aiController);
    }

    public class IdleAIState : AIState
    {
        public override void ActiveState(AIController aiController)
        {
        }
    }

    public class AttackAIState : AIState
    {
        public override void ActiveState(AIController aiController)
        {

        }
    }

    public class UpgradeBaseAIState : AIState
    {
        public override void ActiveState(AIController aiController)
        {

        }
    }
}
