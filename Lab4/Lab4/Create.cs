namespace Lab4
{
    public class Create : Element
    {
        public Create(double delay) : base(delay)
        {
        }

        public override void outAct()
        {
            base.outAct();
            base.setTnext(base.getTcurr() + base.getDelay());
            base.getNextElements()[0].inAct();
        }
    }
}