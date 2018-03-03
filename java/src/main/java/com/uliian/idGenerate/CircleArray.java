package com.uliian.idGenerate;

import java.util.concurrent.atomic.AtomicReferenceArray;

public class CircleArray {
    private final int capacity;
    private AtomicReferenceArray<IdSeed> idSeeds;

    public CircleArray(int capacity) {
        if(capacity<2){
            throw new IllegalArgumentException("capacity must bigger than 2");
        }
        this.capacity = capacity;
        this.idSeeds = new AtomicReferenceArray<IdSeed>(capacity);
    }

    public long generateSequence(long timeStamp){
        int ix = (int)(timeStamp % this.capacity);
        IdSeed seed = this.idSeeds.get(ix);
        if(seed==(null)||seed.getTimeStamp() < timeStamp){
            IdSeed newSeed = new IdSeed(timeStamp);
            this.idSeeds.compareAndSet(ix,seed,newSeed);
            return this.idSeeds.get(ix).increment();
        }else if(seed.getTimeStamp()>timeStamp){
            throw new IllegalArgumentException("timestamp cache expired");
        }
        else {
            return seed.increment();
        }
    }
}
