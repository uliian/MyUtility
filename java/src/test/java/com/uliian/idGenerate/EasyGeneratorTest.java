package com.uliian.idGenerate;

import org.junit.Assert;
import org.junit.Test;
import org.junit.rules.Stopwatch;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.Set;
import java.util.concurrent.*;

import static org.junit.Assert.*;

public class EasyGeneratorTest {
    @Test
    public void idGenerator_grow_success() {
        EasyGenerator easyGenerator = new EasyGenerator(10, 60);
        long oldId = -1L;


        for (int i = 0; i < 5000000; i++) {
            IdResult newIdresult = easyGenerator.generateIdResult();
            long newId = newIdresult.generateId();
            if (!(newId > oldId)) {
                System.out.println("fuck");
            }
            assertTrue(newId > oldId);
            oldId = newId;
        }
    }

    @Test
    public void idGenerator_Notsame_success() throws InterruptedException {
        final int TOTAL_THREADS = 16;
        ExecutorService executorService = Executors.newFixedThreadPool(TOTAL_THREADS);

        for (int cc = 0; cc < 10; cc++) {
            long beginTime = System.currentTimeMillis();
            ConcurrentLinkedQueue<Long> queue = new ConcurrentLinkedQueue();

            EasyGenerator easyGenerator = new EasyGenerator(10, 60);

            ArrayList<Callable<Integer>> callables = new ArrayList<>();
            for (int i = 0; i < 100; i++) {
                Callable<Integer> c = () -> {
                    for (int j = 0; j < 50000; j++) {
                        queue.add(easyGenerator.newId());
                    }
                    return 0;
                };
                callables.add(c);
            }

            executorService.invokeAll(callables);
            System.out.println("OK,500 0000 id spend"+(System.currentTimeMillis()-beginTime)+"ms");
            HashSet<Long> ids = new HashSet<>(queue);
            Assert.assertEquals(ids.size(), queue.size());
        }
    }
}
