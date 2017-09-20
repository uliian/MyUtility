package com.uliian.idGenerate;

import java.util.Calendar;

public class EasyGenerator {
    private final CircleArray circleArray;
    private final int nodeId;
    private final long beginTime;

    public EasyGenerator(int nodeId,int timeWait) {
        this.nodeId = nodeId;
        this.circleArray = new CircleArray(timeWait);
        Calendar calendar = Calendar.getInstance();
        calendar.set(2000,1,1,0,0,0);
        beginTime = calendar.getTime().getTime();
    }

    private IdResult GenerateIdResult() throws InterruptedException {
        do{
            long timestamp = System.currentTimeMillis() - beginTime;
            timestamp = timestamp/1000;

            long sequence = this.circleArray.generateSequence(timestamp);
            if (sequence < 1048574)
            {
                IdResult idresult = new IdResult(timestamp,sequence,this.nodeId);
                return idresult;
            }
            Thread.sleep(100);
        }while (true);
    }
}
