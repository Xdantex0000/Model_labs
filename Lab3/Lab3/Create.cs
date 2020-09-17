namespace Lab3
{
    public class Create : Element {

    public Create(double delay) : base(delay) { }

    public override void outAct() {
        super.outAct();
        super.setTnext(super.getTcurr() + super.getDelay());
        super.getNextElement().inAct();
    }
    }
}