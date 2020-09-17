using System;
using System.Collections.Generic;

namespace Lab3
{
    public class Model {
    
        private List<Element> list = new List<Element>();
        double tnext, tcurr;
        int Event;
    
        public Model(ArrayList<Element> elements) {
            list = elements;
            tnext = 0.0;
            Event = 0;
            tcurr = tnext;
        }
    
        
        public void simulate(double time) {
    
            while (tcurr < time) {
                tnext = Double.MAX_VALUE;
                for (Element e : list) {
                    if (e.getTnext() < tnext) {
                        tnext = e.getTnext();
                        Event = e.getId();
    
                    }
                }
                System.out.println("\nIt's time for Event in " +
      list.get(Event).getName() + 
      ", time =   " + tnext);
                for (Element e : list) {
                    e.doStatistics(tnext - tcurr);
                }
                tcurr = tnext;
                for (Element e : list) {
                    e.setTcurr(tcurr);
                }
                list.get(Event).outAct();
                for (Element e : list) {
                    if (e.getTnext() == tcurr) {
                        e.outAct();
                    }
                }
                printInfo();
            }
            printResult();
        }
    
        public void printInfo() {
            for (Element e : list) {
                e.printInfo();
            }
        }
    
        public void printResult() {
            System.out.println("\n-------------RESULTS-------------");
            for (Element e : list) {
                e.printResult();
                if (e instanceof Process) {
                    Process p = (Process) e;
                    System.out.println("mean length of queue = " +
     p.getMeanQueue() / tcurr
                          			 + "\nfailure probability  = " + 
    				p.getFailure() / (double) p.getQuantity());
                }
            }
        }
    }
}