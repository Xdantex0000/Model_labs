namespace Lab3
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
            var process = (Process)base.getNextElements()[0];
            process.inAct();
        }
    }
}