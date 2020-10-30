In Token1.fst, the following predicate should be conjuncted (`/\`) to the post conditions of `token1_constructor`, `transfer` and `transferFrom` methods since we don't want to support a `modifies_addresses` clause at the Celestial level:
```
(modifies_cmap_only (Set.singleton self) bst0 bst1)
```