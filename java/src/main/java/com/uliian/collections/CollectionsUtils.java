package com.uliian.collections;

import java.util.*;
import java.util.concurrent.ConcurrentHashMap;
import java.util.function.BiFunction;
import java.util.function.Function;
import java.util.function.Predicate;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class CollectionsUtils {
    public static <T> Predicate<T> distinctByKey(Function<? super T, ?> keyExtractor) {
        Set<Object> seen = ConcurrentHashMap.newKeySet();
        return t -> seen.add(keyExtractor.apply(t));
    }
    public static <Outer, Inner, Key, Result> Stream<Result> join(
            Stream<Outer> outer, Stream<Inner> inner,
            Function<Outer, Key> outerKeyFunc,
            Function<Inner, Key> innerKeyFunc,
            BiFunction<Outer, Inner, Result> resultFunc) {

        //Collect the Inner values into a list as we'll need them repeatedly
        List<Inner> innerList = inner.collect(Collectors.toList());

        //matches will store the matches between inner and outer
        final Map<Outer, List<Inner>> matches = new HashMap<>();

        //results will be used to collect the results in
        final List<Result> results = new ArrayList<>();


        outer.forEach(o -> innerList
                .stream()
                //Filter to get those Inners for which the Key equals the Key of this Outer
                .filter(i -> innerKeyFunc.apply(i).equals(outerKeyFunc.apply(o)))
                .forEach(i -> {
                    if (matches.containsKey(o)) {
                        //This Outer already had matches, so add this Inner to the List
                        matches.get(o).add(i);
                    } else {
                        //This is the first Inner to match this Outer, so create a List
                        List<Inner> list = new ArrayList<>();
                        list.add(i);
                        matches.put(o, list);
                    }
                }));

        matches.forEach((out, in) -> in.stream()
                //Map each (Outer, Inner) pair to the appropriate Result...
                .map(i -> resultFunc.apply(out, i))
                //...and collect them
                .forEach(res -> results.add(res)));

        //Return the result as a Stream, like the .NET method does (IEnumerable)
        return results.stream();
    }

}
