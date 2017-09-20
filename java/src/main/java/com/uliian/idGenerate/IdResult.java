package com.uliian.idGenerate;

public class IdResult {
    private long timeStamp;

    public long getTimeStamp() {
        return timeStamp;
    }

    public long getSequence() {
        return sequence;
    }

    public long getNodeId() {
        return nodeId;
    }

    public IdResult(long timeStamp, long sequence, long nodeId) {

        this.timeStamp = timeStamp;
        this.sequence = sequence;
        this.nodeId = nodeId;
    }

    private long sequence;
    private long nodeId;

    public long generateId(){
        return 0 | this.timeStamp << 31 | this.sequence << 10 | this.nodeId;
    }
}
