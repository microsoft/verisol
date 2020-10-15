(*Code generated by compiler*)

module Sample

open FStar.Celestial
open FStar.Celestial.Effect
module CM = FStar.Celestial.ContractsMap
open FStar.Mul
open OtherContract
module M = FStar.Celestial.Map
module L = FStar.List.Tot
module A = FStar.Celestial.Array


noeq type t_sample = {
  sample_m : (m:(M.t othercontract_address bool lt){M.def_of m == false /\ ~ (M.contains m null)});
  sample_t : othercontract_address;
}

(* Contract address type, liveness, and field range macros *)

type sample_address = contract t_sample
let sample_live (c:sample_address) (bst:bstate) =
  c `CM.live_in` bst.cmap
  /\ (let cs = CM.sel c bst.cmap in
    (forall (i:othercontract_address). M.contains cs.sample_m i ==> i `CM.live_in` bst.cmap /\ i <> c)
  )

let sample_fields_in_range (self:sample_address) (bst:bstate{self `sample_live` bst}) : Type0 =
  let cs = CM.sel self bst.cmap in
    False

(* Field getters for contract Sample *)

let sample_get_m (c:sample_address)
: StEth (m:(M.t othercontract_address bool lt){M.def_of m == false /\ ~ (M.contains m null)})
  (fun st -> c `sample_live` st.current)
  (fun st0 r st1 ->
    st0 == st1 /\ r == (CM.sel c st0.current.cmap).sample_m)
= let sample_inst = get_contract c in
  sample_inst.sample_m

let sample_get_t (c:sample_address)
: StEth othercontract_address
  (fun st -> c `sample_live` st.current)
  (fun st0 r st1 ->
    st0 == st1 /\ r == (CM.sel c st0.current.cmap).sample_t)
= let sample_inst = get_contract c in
  sample_inst.sample_t

(* Field setters for contract Sample *)

let sample_set_m (c:sample_address) (_m:(m:(M.t othercontract_address bool lt){M.def_of m == false /\ ~ (M.contains m null)}))
: StEth unit
  (fun st -> c `sample_live` st.current
              /\ (forall (i:othercontract_address). M.contains _m i ==> othercontract_live i st.current /\ i <> c))
  (fun st0 _ st1 ->
    modifies_cmap_only (Set.singleton c) st0.current st1.current /\
    sample_live c st1.current /\
    (let instance0 = CM.sel c st0.current.cmap in
     let instance1 = CM.sel c st1.current.cmap in
    instance1 == { instance0 with sample_m = _m }))
= let sample_inst = get_contract c in
  let sample_inst = { sample_inst with sample_m = _m } in
  set_contract c sample_inst

let sample_set_t (c:sample_address) (_t:othercontract_address)
: StEth unit
  (fun st -> c `sample_live` st.current)
  (fun st0 _ st1 ->
    modifies_cmap_only (Set.singleton c) st0.current st1.current /\
    sample_live c st1.current /\
    (let instance0 = CM.sel c st0.current.cmap in
     let instance1 = CM.sel c st1.current.cmap in
    instance1 == { instance0 with sample_t = _t }))
= let sample_inst = get_contract c in
  let sample_inst = { sample_inst with sample_t = _t } in
  set_contract c sample_inst

let access_contract_instance_1 (_m:tuple2 (m:(M.t othercontract_address bool lt){M.def_of m == false /\ ~ (M.contains m null)}) bstate{forall (k:othercontract_address). M.contains (fst _m) k ==> k `CM.live_in` (snd _m).cmap}) (_a:address)
= ((M.contains (fst _m) _a)) /\ (((CM.sel _a (snd _m).cmap).othercontract_otherContractField) == 5)

let check_external_call (self:sample_address) (sender:address) (value:int) (now:int) (_a:address)
: Eth unit
  (fun bst ->
    sample_live self bst /\
    (sender <> null) /\ (value <= uint_max) /\ (now >= 0) /\ (now <= uint_max) /\ (sender <> self) /\ (value >= 0)
  )
  (fun bst -> False)
  (fun bst0 x bst1 ->
    sample_live self bst1 /\ (
    let cs0 = CM.sel self bst0.cmap in
    let cs1 = CM.sel self bst1.cmap in
    let b0 = pure_get_balance_bst self bst0 in
    let b1 = pure_get_balance_bst self bst1 in
    let l0 = bst0.log in
    let l1 = bst1.log in
      (sample_fields_in_range self bst1)
      /\ ((access_contract_instance_1 (cs0.sample_m, bst0) _a))
      /\ (bst0.balances == bst1.balances)
  ))
=
let cs = get_contract self in
let balance = get_balance self in
let localAddressVariable:address = (null) in
let x1 = ((
        let x1 = (cs.sample_m) in
if M.contains x1 _a then _a else null) = null) in
let _ = (if x1 then begin
revert "address passed is not of type OtherContract";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let localOtherContractInstance:othercontract_address = (null) in
let x1 = (
      let x1 = (cs.sample_m) in
if M.contains x1 _a then _a else null) in
let localOtherContractInstance = x1 in
let cs = get_contract self in
let bst = (get ()).current in
assume (othercontract_fields_in_range localOtherContractInstance bst);
assume (sender <> localOtherContractInstance);
let x1 = setOtherContractField localOtherContractInstance sender 0 now (5) in
let localAddressVariable = x1 in
let balance = get_balance self in
()