package com.uliian.idGenerate;

import java.util.concurrent.atomic.AtomicLong;

class IdSeed {
    private final long timeStamp;
    private AtomicLong sequence;

    public IdSeed(long timeStamp) {
        this.timeStamp = timeStamp;
        this.sequence = new AtomicLong(0);
    }

    public long getTimeStamp(){
        return this.timeStamp;
    }

    public long increment(){
        return this.sequence.getAndIncrement();
    }
}
