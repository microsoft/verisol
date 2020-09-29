(*Code generated by compiler*)

module EtherDelta_Cel

open FStar.Celestial
open FStar.Celestial.Effect
module CM = FStar.Celestial.ContractsMap
open FStar.Mul
module M = FStar.Celestial.Map
module L = FStar.List.Tot
module A = FStar.Celestial.Array

assume val etherdelta_cel_Deposit : string
assume val etherdelta_cel_Withdraw : string

noeq type t_etherdelta_cel = {
  etherdelta_cel_admin : address;
  etherdelta_cel_feeAccount : address;
  etherdelta_cel_feeMake : uint;
  etherdelta_cel_feeTake : uint;
  etherdelta_cel_feeRebate : uint;
  etherdelta_cel_tokens : (m:(M.t uint (m:(M.t address uint lt){M.def_of m == 0}) lt){M.def_of m == M.const (0)});
  etherdelta_cel_tokenTxStatus : bool;
  etherdelta_cel_totalBalance : uint;
  etherdelta_cel__lock_ : bool;
}

(* Contract address type, liveness, and field range macros *)

type etherdelta_cel_address = contract t_etherdelta_cel
let etherdelta_cel_live (c:etherdelta_cel_address) (bst:bstate) =
  c `CM.live_in` bst.cmap

(* Field getters for contract EtherDelta_Cel *)

let etherdelta_cel_get_admin (c:etherdelta_cel_address)
: StEth address
  (fun st -> c `etherdelta_cel_live` st.current)
  (fun st0 r st1 ->
    st0 == st1 /\ r == (CM.sel c st0.current.cmap).etherdelta_cel_admin)
= let etherdelta_cel_inst = get_contract c in
  etherdelta_cel_inst.etherdelta_cel_admin

let etherdelta_cel_get_feeAccount (c:etherdelta_cel_address)
: StEth address
  (fun st -> c `etherdelta_cel_live` st.current)
  (fun st0 r st1 ->
    st0 == st1 /\ r == (CM.sel c st0.current.cmap).etherdelta_cel_feeAccount)
= let etherdelta_cel_inst = get_contract c in
  etherdelta_cel_inst.etherdelta_cel_feeAccount

let etherdelta_cel_get_feeMake (c:etherdelta_cel_address)
: StEth uint
  (fun st -> c `etherdelta_cel_live` st.current)
  (fun st0 r st1 ->
    st0 == st1 /\ r == (CM.sel c st0.current.cmap).etherdelta_cel_feeMake)
= let etherdelta_cel_inst = get_contract c in
  etherdelta_cel_inst.etherdelta_cel_feeMake

let etherdelta_cel_get_feeTake (c:etherdelta_cel_address)
: StEth uint
  (fun st -> c `etherdelta_cel_live` st.current)
  (fun st0 r st1 ->
    st0 == st1 /\ r == (CM.sel c st0.current.cmap).etherdelta_cel_feeTake)
= let etherdelta_cel_inst = get_contract c in
  etherdelta_cel_inst.etherdelta_cel_feeTake

let etherdelta_cel_get_feeRebate (c:etherdelta_cel_address)
: StEth uint
  (fun st -> c `etherdelta_cel_live` st.current)
  (fun st0 r st1 ->
    st0 == st1 /\ r == (CM.sel c st0.current.cmap).etherdelta_cel_feeRebate)
= let etherdelta_cel_inst = get_contract c in
  etherdelta_cel_inst.etherdelta_cel_feeRebate

let etherdelta_cel_get_tokens (c:etherdelta_cel_address)
: StEth (m:(M.t uint (m:(M.t address uint lt){M.def_of m == 0}) lt){M.def_of m == M.const (0)})
  (fun st -> c `etherdelta_cel_live` st.current)
  (fun st0 r st1 ->
    st0 == st1 /\ r == (CM.sel c st0.current.cmap).etherdelta_cel_tokens)
= let etherdelta_cel_inst = get_contract c in
  etherdelta_cel_inst.etherdelta_cel_tokens

let etherdelta_cel_get_tokenTxStatus (c:etherdelta_cel_address)
: StEth bool
  (fun st -> c `etherdelta_cel_live` st.current)
  (fun st0 r st1 ->
    st0 == st1 /\ r == (CM.sel c st0.current.cmap).etherdelta_cel_tokenTxStatus)
= let etherdelta_cel_inst = get_contract c in
  etherdelta_cel_inst.etherdelta_cel_tokenTxStatus

let etherdelta_cel_get_totalBalance (c:etherdelta_cel_address)
: StEth uint
  (fun st -> c `etherdelta_cel_live` st.current)
  (fun st0 r st1 ->
    st0 == st1 /\ r == (CM.sel c st0.current.cmap).etherdelta_cel_totalBalance)
= let etherdelta_cel_inst = get_contract c in
  etherdelta_cel_inst.etherdelta_cel_totalBalance

let etherdelta_cel_get__lock_ (c:etherdelta_cel_address)
: StEth bool
  (fun st -> c `etherdelta_cel_live` st.current)
  (fun st0 r st1 ->
    st0 == st1 /\ r == (CM.sel c st0.current.cmap).etherdelta_cel__lock_)
= let etherdelta_cel_inst = get_contract c in
  etherdelta_cel_inst.etherdelta_cel__lock_

(* Field setters for contract EtherDelta_Cel *)

let etherdelta_cel_set_admin (c:etherdelta_cel_address) (_admin:address)
: StEth unit
  (fun st -> c `etherdelta_cel_live` st.current)
  (fun st0 _ st1 ->
    modifies_cmap_only (Set.singleton c) st0.current st1.current /\
    etherdelta_cel_live c st1.current /\
    (let instance0 = CM.sel c st0.current.cmap in
     let instance1 = CM.sel c st1.current.cmap in
    instance1 == { instance0 with etherdelta_cel_admin = _admin }))
= let etherdelta_cel_inst = get_contract c in
  let etherdelta_cel_inst = { etherdelta_cel_inst with etherdelta_cel_admin = _admin } in
  set_contract c etherdelta_cel_inst

let etherdelta_cel_set_feeAccount (c:etherdelta_cel_address) (_feeAccount:address)
: StEth unit
  (fun st -> c `etherdelta_cel_live` st.current)
  (fun st0 _ st1 ->
    modifies_cmap_only (Set.singleton c) st0.current st1.current /\
    etherdelta_cel_live c st1.current /\
    (let instance0 = CM.sel c st0.current.cmap in
     let instance1 = CM.sel c st1.current.cmap in
    instance1 == { instance0 with etherdelta_cel_feeAccount = _feeAccount }))
= let etherdelta_cel_inst = get_contract c in
  let etherdelta_cel_inst = { etherdelta_cel_inst with etherdelta_cel_feeAccount = _feeAccount } in
  set_contract c etherdelta_cel_inst

let etherdelta_cel_set_feeMake (c:etherdelta_cel_address) (_feeMake:uint)
: StEth unit
  (fun st -> c `etherdelta_cel_live` st.current
              /\ _feeMake >= 0 /\ _feeMake <= uint_max)
  (fun st0 _ st1 ->
    modifies_cmap_only (Set.singleton c) st0.current st1.current /\
    etherdelta_cel_live c st1.current /\
    (let instance0 = CM.sel c st0.current.cmap in
     let instance1 = CM.sel c st1.current.cmap in
    instance1 == { instance0 with etherdelta_cel_feeMake = _feeMake }))
= let etherdelta_cel_inst = get_contract c in
  let etherdelta_cel_inst = { etherdelta_cel_inst with etherdelta_cel_feeMake = _feeMake } in
  set_contract c etherdelta_cel_inst

let etherdelta_cel_set_feeTake (c:etherdelta_cel_address) (_feeTake:uint)
: StEth unit
  (fun st -> c `etherdelta_cel_live` st.current
              /\ _feeTake >= 0 /\ _feeTake <= uint_max)
  (fun st0 _ st1 ->
    modifies_cmap_only (Set.singleton c) st0.current st1.current /\
    etherdelta_cel_live c st1.current /\
    (let instance0 = CM.sel c st0.current.cmap in
     let instance1 = CM.sel c st1.current.cmap in
    instance1 == { instance0 with etherdelta_cel_feeTake = _feeTake }))
= let etherdelta_cel_inst = get_contract c in
  let etherdelta_cel_inst = { etherdelta_cel_inst with etherdelta_cel_feeTake = _feeTake } in
  set_contract c etherdelta_cel_inst

let etherdelta_cel_set_feeRebate (c:etherdelta_cel_address) (_feeRebate:uint)
: StEth unit
  (fun st -> c `etherdelta_cel_live` st.current
              /\ _feeRebate >= 0 /\ _feeRebate <= uint_max)
  (fun st0 _ st1 ->
    modifies_cmap_only (Set.singleton c) st0.current st1.current /\
    etherdelta_cel_live c st1.current /\
    (let instance0 = CM.sel c st0.current.cmap in
     let instance1 = CM.sel c st1.current.cmap in
    instance1 == { instance0 with etherdelta_cel_feeRebate = _feeRebate }))
= let etherdelta_cel_inst = get_contract c in
  let etherdelta_cel_inst = { etherdelta_cel_inst with etherdelta_cel_feeRebate = _feeRebate } in
  set_contract c etherdelta_cel_inst

let etherdelta_cel_set_tokens (c:etherdelta_cel_address) (_tokens:(m:(M.t uint (m:(M.t address uint lt){M.def_of m == 0}) lt){M.def_of m == M.const (0)}))
: StEth unit
  (fun st -> c `etherdelta_cel_live` st.current)
  (fun st0 _ st1 ->
    modifies_cmap_only (Set.singleton c) st0.current st1.current /\
    etherdelta_cel_live c st1.current /\
    (let instance0 = CM.sel c st0.current.cmap in
     let instance1 = CM.sel c st1.current.cmap in
    instance1 == { instance0 with etherdelta_cel_tokens = _tokens }))
= let etherdelta_cel_inst = get_contract c in
  let etherdelta_cel_inst = { etherdelta_cel_inst with etherdelta_cel_tokens = _tokens } in
  set_contract c etherdelta_cel_inst

let etherdelta_cel_set_tokenTxStatus (c:etherdelta_cel_address) (_tokenTxStatus:bool)
: StEth unit
  (fun st -> c `etherdelta_cel_live` st.current)
  (fun st0 _ st1 ->
    modifies_cmap_only (Set.singleton c) st0.current st1.current /\
    etherdelta_cel_live c st1.current /\
    (let instance0 = CM.sel c st0.current.cmap in
     let instance1 = CM.sel c st1.current.cmap in
    instance1 == { instance0 with etherdelta_cel_tokenTxStatus = _tokenTxStatus }))
= let etherdelta_cel_inst = get_contract c in
  let etherdelta_cel_inst = { etherdelta_cel_inst with etherdelta_cel_tokenTxStatus = _tokenTxStatus } in
  set_contract c etherdelta_cel_inst

let etherdelta_cel_set_totalBalance (c:etherdelta_cel_address) (_totalBalance:uint)
: StEth unit
  (fun st -> c `etherdelta_cel_live` st.current
              /\ _totalBalance >= 0 /\ _totalBalance <= uint_max)
  (fun st0 _ st1 ->
    modifies_cmap_only (Set.singleton c) st0.current st1.current /\
    etherdelta_cel_live c st1.current /\
    (let instance0 = CM.sel c st0.current.cmap in
     let instance1 = CM.sel c st1.current.cmap in
    instance1 == { instance0 with etherdelta_cel_totalBalance = _totalBalance }))
= let etherdelta_cel_inst = get_contract c in
  let etherdelta_cel_inst = { etherdelta_cel_inst with etherdelta_cel_totalBalance = _totalBalance } in
  set_contract c etherdelta_cel_inst

let etherdelta_cel_set__lock_ (c:etherdelta_cel_address) (__lock_:bool)
: StEth unit
  (fun st -> c `etherdelta_cel_live` st.current)
  (fun st0 _ st1 ->
    modifies_cmap_only (Set.singleton c) st0.current st1.current /\
    etherdelta_cel_live c st1.current /\
    (let instance0 = CM.sel c st0.current.cmap in
     let instance1 = CM.sel c st1.current.cmap in
    instance1 == { instance0 with etherdelta_cel__lock_ = __lock_ }))
= let etherdelta_cel_inst = get_contract c in
  let etherdelta_cel_inst = { etherdelta_cel_inst with etherdelta_cel__lock_ = __lock_ } in
  set_contract c etherdelta_cel_inst

let eth_balances2 (self:etherdelta_cel_address) (bst:bstate{self `etherdelta_cel_live` bst}) : Type0 =
  let etherdelta_cel_balance = pure_get_balance_bst self bst in
  let cs = CM.sel self bst.cmap in
    etherdelta_cel_balance == cs.etherdelta_cel_totalBalance

let eth_balances (self:etherdelta_cel_address) (bst:bstate{self `etherdelta_cel_live` bst}) : Type0 =
  let etherdelta_cel_balance = pure_get_balance_bst self bst in
  let cs = CM.sel self bst.cmap in
    cs.etherdelta_cel_totalBalance >= ((sum_mapping (M.sel cs.etherdelta_cel_tokens 0)))

let etherdelta_cel_constructor (self:etherdelta_cel_address) (sender:address) (value:uint) (tx:tx) (block:block) (admin_:address) (feeAccount_:address) (feeMake_:uint) (feeTake_:uint) (feeRebate_:uint)
: Eth1 unit
  (fun bst -> 
    etherdelta_cel_live self bst /\
    (let b = pure_get_balance_bst self bst in
    let cs = CM.sel self bst.cmap in
      (sender <> null)
      /\ (cs.etherdelta_cel_admin == null)
      /\ (cs.etherdelta_cel_feeAccount == null)
      /\ (cs.etherdelta_cel_feeMake == 0)
      /\ (cs.etherdelta_cel_feeTake == 0)
      /\ (cs.etherdelta_cel_feeRebate == 0)
      /\ (cs.etherdelta_cel_tokens == M.const (M.const (0)))
      /\ (cs.etherdelta_cel_tokenTxStatus == false)
      /\ (cs.etherdelta_cel_totalBalance == 0)
      /\ (cs.etherdelta_cel__lock_ == false)
      /\ ((b == 0))
    )
  )
  (fun bst -> False)
  (fun bst0 x bst1 ->
    etherdelta_cel_live self bst1
    /\ (eth_balances2 self bst1)
    /\ (eth_balances self bst1)
  )
=
let cs = get_contract self in
let balance = get_balance self in
let _ = etherdelta_cel_set_admin self admin_ in
let cs = get_contract self in
let _ = etherdelta_cel_set_feeAccount self feeAccount_ in
let cs = get_contract self in
let _ = etherdelta_cel_set_feeMake self feeMake_ in
let cs = get_contract self in
let _ = etherdelta_cel_set_feeTake self feeTake_ in
let cs = get_contract self in
let _ = etherdelta_cel_set_feeRebate self feeRebate_ in
let cs = get_contract self in
()

let changeAdmin (self:etherdelta_cel_address) (sender:address{sender <> null}) (value:uint) (tx:tx) (block:block) (admin_:address)
: Eth1 unit
  (fun bst ->
    etherdelta_cel_live self bst /\ (
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
      (eth_balances2 self bst)
      /\ (eth_balances self bst)
  ))
  (fun bst ->
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
    ((sender =!= cs.etherdelta_cel_admin))
    \/ (cs.etherdelta_cel__lock_)
  )
  (fun bst0 x bst1 ->
    etherdelta_cel_live self bst1 /\ (
    let cs0 = CM.sel self bst0.cmap in
    let cs1 = CM.sel self bst1.cmap in
    let b0 = pure_get_balance_bst self bst0 in
    let b1 = pure_get_balance_bst self bst1 in
    let l0 = bst0.log in
    let l1 = bst1.log in
    (eth_balances2 self bst1)
      /\ (eth_balances self bst1)
      /\ ((cs1.etherdelta_cel_admin == admin_))
      /\ (bst0.balances == bst1.balances)
      /\ (l0 == l1)
      /\ (cs0.etherdelta_cel_feeTake == cs1.etherdelta_cel_feeTake)
      /\ (cs0.etherdelta_cel__lock_ == cs1.etherdelta_cel__lock_)
      /\ (cs0.etherdelta_cel_totalBalance == cs1.etherdelta_cel_totalBalance)
      /\ (cs0.etherdelta_cel_tokenTxStatus == cs1.etherdelta_cel_tokenTxStatus)
      /\ (cs0.etherdelta_cel_feeRebate == cs1.etherdelta_cel_feeRebate)
      /\ (cs0.etherdelta_cel_feeAccount == cs1.etherdelta_cel_feeAccount)
      /\ (cs0.etherdelta_cel_feeMake == cs1.etherdelta_cel_feeMake)
      /\ (cs0.etherdelta_cel_tokens == cs1.etherdelta_cel_tokens)
  ))
=
let cs = get_contract self in
let balance = get_balance self in
let _ = (if (cs.etherdelta_cel__lock_) then begin
revert "Reentrancy detected";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let _ = (if (sender <> cs.etherdelta_cel_admin) then begin
revert "invalid";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let _ = etherdelta_cel_set_admin self admin_ in
let cs = get_contract self in
()

let changeFeeAccount (self:etherdelta_cel_address) (sender:address{sender <> null}) (value:uint) (tx:tx) (block:block) (feeAccount_:address)
: Eth1 unit
  (fun bst ->
    etherdelta_cel_live self bst /\ (
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
      (eth_balances2 self bst)
      /\ (eth_balances self bst)
  ))
  (fun bst ->
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
    ((sender =!= cs.etherdelta_cel_admin))
    \/ (cs.etherdelta_cel__lock_)
  )
  (fun bst0 x bst1 ->
    etherdelta_cel_live self bst1 /\ (
    let cs0 = CM.sel self bst0.cmap in
    let cs1 = CM.sel self bst1.cmap in
    let b0 = pure_get_balance_bst self bst0 in
    let b1 = pure_get_balance_bst self bst1 in
    let l0 = bst0.log in
    let l1 = bst1.log in
    (eth_balances2 self bst1)
      /\ (eth_balances self bst1)
      /\ (bst0.balances == bst1.balances)
      /\ (l0 == l1)
      /\ (cs0.etherdelta_cel_feeTake == cs1.etherdelta_cel_feeTake)
      /\ (cs0.etherdelta_cel__lock_ == cs1.etherdelta_cel__lock_)
      /\ (cs0.etherdelta_cel_totalBalance == cs1.etherdelta_cel_totalBalance)
      /\ (cs0.etherdelta_cel_tokenTxStatus == cs1.etherdelta_cel_tokenTxStatus)
      /\ (cs0.etherdelta_cel_feeRebate == cs1.etherdelta_cel_feeRebate)
      /\ (cs0.etherdelta_cel_feeMake == cs1.etherdelta_cel_feeMake)
      /\ (cs0.etherdelta_cel_tokens == cs1.etherdelta_cel_tokens)
      /\ (cs0.etherdelta_cel_admin == cs1.etherdelta_cel_admin)
  ))
=
let cs = get_contract self in
let balance = get_balance self in
let _ = (if (cs.etherdelta_cel__lock_) then begin
revert "Reentrancy detected";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let _ = (if (sender <> cs.etherdelta_cel_admin) then begin
revert "invalid";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let _ = etherdelta_cel_set_feeAccount self feeAccount_ in
let cs = get_contract self in
()

let changeFeeMake (self:etherdelta_cel_address) (sender:address{sender <> null}) (value:uint) (tx:tx) (block:block) (feeMake_:uint)
: Eth1 unit
  (fun bst ->
    etherdelta_cel_live self bst /\ (
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
      (eth_balances2 self bst)
      /\ (eth_balances self bst)
  ))
  (fun bst ->
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
    (((sender =!= cs.etherdelta_cel_admin) \/ (feeMake_ > cs.etherdelta_cel_feeMake)))
    \/ (cs.etherdelta_cel__lock_)
  )
  (fun bst0 x bst1 ->
    etherdelta_cel_live self bst1 /\ (
    let cs0 = CM.sel self bst0.cmap in
    let cs1 = CM.sel self bst1.cmap in
    let b0 = pure_get_balance_bst self bst0 in
    let b1 = pure_get_balance_bst self bst1 in
    let l0 = bst0.log in
    let l1 = bst1.log in
    (eth_balances2 self bst1)
      /\ (eth_balances self bst1)
      /\ (cs1.etherdelta_cel_feeMake == feeMake_)
      /\ (bst0.balances == bst1.balances)
      /\ (l0 == l1)
      /\ (cs0.etherdelta_cel_feeTake == cs1.etherdelta_cel_feeTake)
      /\ (cs0.etherdelta_cel__lock_ == cs1.etherdelta_cel__lock_)
      /\ (cs0.etherdelta_cel_totalBalance == cs1.etherdelta_cel_totalBalance)
      /\ (cs0.etherdelta_cel_tokenTxStatus == cs1.etherdelta_cel_tokenTxStatus)
      /\ (cs0.etherdelta_cel_feeRebate == cs1.etherdelta_cel_feeRebate)
      /\ (cs0.etherdelta_cel_feeAccount == cs1.etherdelta_cel_feeAccount)
      /\ (cs0.etherdelta_cel_tokens == cs1.etherdelta_cel_tokens)
      /\ (cs0.etherdelta_cel_admin == cs1.etherdelta_cel_admin)
  ))
=
let cs = get_contract self in
let balance = get_balance self in
let _ = (if (cs.etherdelta_cel__lock_) then begin
revert "Reentrancy detected";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let _ = (if ((sender <> cs.etherdelta_cel_admin) || (feeMake_ > cs.etherdelta_cel_feeMake)) then begin
revert "invalid";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let _ = etherdelta_cel_set_feeMake self feeMake_ in
let cs = get_contract self in
()

let changeFeeTake (self:etherdelta_cel_address) (sender:address{sender <> null}) (value:uint) (tx:tx) (block:block) (feeTake_:uint)
: Eth1 unit
  (fun bst ->
    etherdelta_cel_live self bst /\ (
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
      (eth_balances2 self bst)
      /\ (eth_balances self bst)
  ))
  (fun bst ->
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
    ((((sender =!= cs.etherdelta_cel_admin) \/ (feeTake_ > cs.etherdelta_cel_feeTake)) \/ (feeTake_ < cs.etherdelta_cel_feeRebate)))
    \/ (cs.etherdelta_cel__lock_)
  )
  (fun bst0 x bst1 ->
    etherdelta_cel_live self bst1 /\ (
    let cs0 = CM.sel self bst0.cmap in
    let cs1 = CM.sel self bst1.cmap in
    let b0 = pure_get_balance_bst self bst0 in
    let b1 = pure_get_balance_bst self bst1 in
    let l0 = bst0.log in
    let l1 = bst1.log in
    (eth_balances2 self bst1)
      /\ (eth_balances self bst1)
      /\ (cs1.etherdelta_cel_feeTake == feeTake_)
      /\ (bst0.balances == bst1.balances)
      /\ (l0 == l1)
      /\ (cs0.etherdelta_cel__lock_ == cs1.etherdelta_cel__lock_)
      /\ (cs0.etherdelta_cel_totalBalance == cs1.etherdelta_cel_totalBalance)
      /\ (cs0.etherdelta_cel_tokenTxStatus == cs1.etherdelta_cel_tokenTxStatus)
      /\ (cs0.etherdelta_cel_feeRebate == cs1.etherdelta_cel_feeRebate)
      /\ (cs0.etherdelta_cel_feeAccount == cs1.etherdelta_cel_feeAccount)
      /\ (cs0.etherdelta_cel_feeMake == cs1.etherdelta_cel_feeMake)
      /\ (cs0.etherdelta_cel_tokens == cs1.etherdelta_cel_tokens)
      /\ (cs0.etherdelta_cel_admin == cs1.etherdelta_cel_admin)
  ))
=
let cs = get_contract self in
let balance = get_balance self in
let _ = (if (cs.etherdelta_cel__lock_) then begin
revert "Reentrancy detected";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let _ = (if (((sender <> cs.etherdelta_cel_admin) || (feeTake_ > cs.etherdelta_cel_feeTake)) || (feeTake_ < cs.etherdelta_cel_feeRebate)) then begin
revert "invalid";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let _ = etherdelta_cel_set_feeTake self feeTake_ in
let cs = get_contract self in
()

let changeFeeRebate (self:etherdelta_cel_address) (sender:address{sender <> null}) (value:uint) (tx:tx) (block:block) (feeRebate_:uint)
: Eth1 unit
  (fun bst ->
    etherdelta_cel_live self bst /\ (
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
      (eth_balances2 self bst)
      /\ (eth_balances self bst)
  ))
  (fun bst ->
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
    ((((sender =!= cs.etherdelta_cel_admin) \/ (feeRebate_ < cs.etherdelta_cel_feeRebate)) \/ (feeRebate_ > cs.etherdelta_cel_feeTake)))
    \/ (cs.etherdelta_cel__lock_)
  )
  (fun bst0 x bst1 ->
    etherdelta_cel_live self bst1 /\ (
    let cs0 = CM.sel self bst0.cmap in
    let cs1 = CM.sel self bst1.cmap in
    let b0 = pure_get_balance_bst self bst0 in
    let b1 = pure_get_balance_bst self bst1 in
    let l0 = bst0.log in
    let l1 = bst1.log in
    (eth_balances2 self bst1)
      /\ (eth_balances self bst1)
      /\ (cs1.etherdelta_cel_feeRebate == feeRebate_)
      /\ (bst0.balances == bst1.balances)
      /\ (l0 == l1)
      /\ (cs0.etherdelta_cel_feeTake == cs1.etherdelta_cel_feeTake)
      /\ (cs0.etherdelta_cel__lock_ == cs1.etherdelta_cel__lock_)
      /\ (cs0.etherdelta_cel_totalBalance == cs1.etherdelta_cel_totalBalance)
      /\ (cs0.etherdelta_cel_tokenTxStatus == cs1.etherdelta_cel_tokenTxStatus)
      /\ (cs0.etherdelta_cel_feeAccount == cs1.etherdelta_cel_feeAccount)
      /\ (cs0.etherdelta_cel_feeMake == cs1.etherdelta_cel_feeMake)
      /\ (cs0.etherdelta_cel_tokens == cs1.etherdelta_cel_tokens)
      /\ (cs0.etherdelta_cel_admin == cs1.etherdelta_cel_admin)
  ))
=
let cs = get_contract self in
let balance = get_balance self in
let _ = (if (cs.etherdelta_cel__lock_) then begin
revert "Reentrancy detected";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let _ = (if (((sender <> cs.etherdelta_cel_admin) || (feeRebate_ < cs.etherdelta_cel_feeRebate)) || (feeRebate_ > cs.etherdelta_cel_feeTake)) then begin
revert "invalid";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let _ = etherdelta_cel_set_feeRebate self feeRebate_ in
let cs = get_contract self in
()

let deposit (self:etherdelta_cel_address) (sender:address{sender <> null}) (value:uint) (tx:tx) (block:block)
: Eth1 unit
  (fun bst ->
    etherdelta_cel_live self bst /\ (
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
      (eth_balances2 self bst)
      /\ (eth_balances self bst)
  ))
  (fun bst ->
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
    (((cs.etherdelta_cel_totalBalance + value) > uint_max))
    \/ (cs.etherdelta_cel__lock_)
  )
  (fun bst0 x bst1 ->
    etherdelta_cel_live self bst1 /\ (
    let cs0 = CM.sel self bst0.cmap in
    let cs1 = CM.sel self bst1.cmap in
    let b0 = pure_get_balance_bst self bst0 in
    let b1 = pure_get_balance_bst self bst1 in
    let l0 = bst0.log in
    let l1 = bst1.log in
    (eth_balances2 self bst1)
      /\ (eth_balances self bst1)
      /\ (((M.equal cs1.etherdelta_cel_tokens (
      let x1 = (cs0.etherdelta_cel_tokens) in
      let x2 = (0) in
      let x3 = (
        let x1 = (M.sel cs0.etherdelta_cel_tokens 0) in
        let x2 = (sender) in
        let x3 = (((M.sel (M.sel cs0.etherdelta_cel_tokens 0) sender) + value)) in
        let x1 = (M.upd x1 x2 x3) in
        x1) in
      let x1 = (M.upd x1 x2 x3) in
      x1))) /\ ((cs1.etherdelta_cel_totalBalance == (cs0.etherdelta_cel_totalBalance + value))))
      /\ (cs0.etherdelta_cel_feeTake == cs1.etherdelta_cel_feeTake)
      /\ (cs0.etherdelta_cel__lock_ == cs1.etherdelta_cel__lock_)
      /\ (cs0.etherdelta_cel_tokenTxStatus == cs1.etherdelta_cel_tokenTxStatus)
      /\ (cs0.etherdelta_cel_feeRebate == cs1.etherdelta_cel_feeRebate)
      /\ (cs0.etherdelta_cel_feeAccount == cs1.etherdelta_cel_feeAccount)
      /\ (cs0.etherdelta_cel_feeMake == cs1.etherdelta_cel_feeMake)
      /\ (cs0.etherdelta_cel_admin == cs1.etherdelta_cel_admin)
  ))
=
let b = get_balance self in
let _ = set_balance self (
          if (b + value > uint_max) then (b + value - uint_max)
          else (b + value)) in
let cs = get_contract self in
let balance = get_balance self in
let _ = (if (cs.etherdelta_cel__lock_) then begin
revert "Reentrancy detected";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let x1 = ((if cs.etherdelta_cel_totalBalance <= uint_max - value then (cs.etherdelta_cel_totalBalance + value) else revert "Overflow error")) in
let _ = etherdelta_cel_set_totalBalance self x1 in
let cs = get_contract self in
let x5 = ((_add (M.sel (M.sel cs.etherdelta_cel_tokens 0) sender) value)) in
let x4 = cs.etherdelta_cel_tokens in

let x3 = (0) in
let x2 = (M.sel x4 x3) in
let x1 = (sender) in
let x0 = (M.sel x2 x1) in

let _ = etherdelta_cel_set_tokens self (M.upd x4 x3 (M.upd x2 x1 x5)) in
let cs = get_contract self in
let _ = emit etherdelta_cel_Deposit (null, sender, value, M.sel (M.sel cs.etherdelta_cel_tokens 0) sender) in
let cs = get_contract self in
let balance = get_balance self in
()

let withdraw (self:etherdelta_cel_address) (sender:address{sender <> null}) (value:uint) (tx:tx) (block:block) (amount:uint)
: Eth1 unit
  (fun bst ->
    etherdelta_cel_live self bst /\ (
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
      (eth_balances2 self bst)
      /\ (eth_balances self bst)
  ))
  (fun bst ->
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
    (((M.sel (M.sel cs.etherdelta_cel_tokens 0) sender) < amount))
    \/ (cs.etherdelta_cel__lock_)
  )
  (fun bst0 x bst1 ->
    etherdelta_cel_live self bst1 /\ (
    let cs0 = CM.sel self bst0.cmap in
    let cs1 = CM.sel self bst1.cmap in
    let b0 = pure_get_balance_bst self bst0 in
    let b1 = pure_get_balance_bst self bst1 in
    let l0 = bst0.log in
    let l1 = bst1.log in
    (eth_balances2 self bst1)
      /\ (eth_balances self bst1)
      /\ (((b0 =!= b1 ==> ((M.equal cs1.etherdelta_cel_tokens (
        let x1 = (cs0.etherdelta_cel_tokens) in
        let x2 = (0) in
        let x3 = (
          let x1 = (M.sel cs0.etherdelta_cel_tokens 0) in
          let x2 = (sender) in
          let x3 = ((M.sel (M.sel cs0.etherdelta_cel_tokens 0) sender) - amount) in
          let x1 = (M.upd x1 x2 x3) in
          x1) in
        let x1 = (M.upd x1 x2 x3) in
        x1))) /\ (l1 == ((mk_event null etherdelta_cel_Withdraw (null, sender, amount, M.sel (M.sel cs1.etherdelta_cel_tokens 0) sender))::(mk_event sender eTransfer amount)::l0)))))
      /\ (b1 <= b0)
  ))
=
let cs = get_contract self in
let balance = get_balance self in
let _ = (if (cs.etherdelta_cel__lock_) then begin
revert "Reentrancy detected";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let _ = (if ((M.sel (M.sel cs.etherdelta_cel_tokens 0) sender) < amount) then begin
revert "Insufficient balance";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let b:uint = (balance) in
let _ = send self sender amount in
let cs = get_contract self in
let balance = get_balance self in
let _ = (if (b <> balance) then begin
let x5 = ((_sub (M.sel (M.sel cs.etherdelta_cel_tokens 0) sender) amount)) in
let x4 = cs.etherdelta_cel_tokens in

let x3 = (0) in
let x2 = (M.sel x4 x3) in
let x1 = (sender) in
let x0 = (M.sel x2 x1) in
  
let _ = etherdelta_cel_set_tokens self (M.upd x4 x3 (M.upd x2 x1 x5)) in
let cs = get_contract self in
let _ = etherdelta_cel_set_totalBalance self (_sub cs.etherdelta_cel_totalBalance amount) in
let cs = get_contract self in
let _ = emit etherdelta_cel_Withdraw (null, sender, amount, M.sel (M.sel cs.etherdelta_cel_tokens 0) sender) in
let cs = get_contract self in
let balance = get_balance self in
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
()

let depositToken (self:etherdelta_cel_address) (sender:address{sender <> null}) (value:uint) (tx:tx) (block:block) (tokenId:uint) (token:address) (amount:uint)
: Eth1 unit
  (fun bst ->
    etherdelta_cel_live self bst /\ (
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
      (eth_balances2 self bst)
      /\ (eth_balances self bst)
  ))
  (fun bst ->
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
    ((tokenId == 0) \/ (((M.sel (M.sel cs.etherdelta_cel_tokens tokenId) sender) + amount) > uint_max))
    \/ (cs.etherdelta_cel__lock_)
  )
  (fun bst0 x bst1 ->
    etherdelta_cel_live self bst1 /\ (
    let cs0 = CM.sel self bst0.cmap in
    let cs1 = CM.sel self bst1.cmap in
    let b0 = pure_get_balance_bst self bst0 in
    let b1 = pure_get_balance_bst self bst1 in
    let l0 = bst0.log in
    let l1 = bst1.log in
    (eth_balances2 self bst1)
      /\ (eth_balances self bst1)
      /\ ((cs1.etherdelta_cel_tokenTxStatus ==> M.equal cs1.etherdelta_cel_tokens (
        let x1 = (cs0.etherdelta_cel_tokens) in
        let x2 = (tokenId) in
        let x3 = (
          let x1 = (M.sel cs0.etherdelta_cel_tokens tokenId) in
          let x2 = (sender) in
          let x3 = ((M.sel (M.sel cs0.etherdelta_cel_tokens tokenId) sender) + amount) in
          let x1 = (M.upd x1 x2 x3) in
          x1) in
        let x1 = (M.upd x1 x2 x3) in
        x1)))
      /\ (cs0.etherdelta_cel_feeTake == cs1.etherdelta_cel_feeTake)
      /\ (cs0.etherdelta_cel_feeRebate == cs1.etherdelta_cel_feeRebate)
      /\ (cs0.etherdelta_cel_feeAccount == cs1.etherdelta_cel_feeAccount)
      /\ (cs0.etherdelta_cel_feeMake == cs1.etherdelta_cel_feeMake)
      /\ (cs0.etherdelta_cel_admin == cs1.etherdelta_cel_admin)
  ))
=
let cs = get_contract self in
let balance = get_balance self in
let _ = (if (cs.etherdelta_cel__lock_) then begin
revert "Reentrancy detected";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let x1 = ((tokenId = 0) || (amount > ((_sub uint_max (M.sel (M.sel cs.etherdelta_cel_tokens tokenId) sender))))) in
let _ = (if x1 then begin
revert "Invalid token type or overflow";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let _ = etherdelta_cel_set__lock_ self true in
let cs = get_contract self in
assert ((cs.etherdelta_cel__lock_));
let x1 = unknown_call self in
let _ = etherdelta_cel_set_tokenTxStatus self (x1) in
let balance = get_balance self in
let _ = etherdelta_cel_set__lock_ self false in
let cs = get_contract self in
let _ = (if (cs.etherdelta_cel_totalBalance > balance) then begin
revert "Unexpected Ether transferred to self";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let x1 = (balance) in
let _ = etherdelta_cel_set_totalBalance self x1 in
let cs = get_contract self in
let x1 = (cs.etherdelta_cel_tokenTxStatus = true) in
let _ = (if x1 then begin
let x5 = ((_add (M.sel (M.sel cs.etherdelta_cel_tokens tokenId) sender) amount)) in
let x4 = cs.etherdelta_cel_tokens in

let x3 = (tokenId) in
let x2 = (M.sel x4 x3) in
let x1 = (sender) in
let x0 = (M.sel x2 x1) in
  
let _ = etherdelta_cel_set_tokens self (M.upd x4 x3 (M.upd x2 x1 x5)) in
let cs = get_contract self in
let _ = emit etherdelta_cel_Deposit (token, sender, amount, M.sel (M.sel cs.etherdelta_cel_tokens tokenId) sender) in
let cs = get_contract self in
let balance = get_balance self in
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
()

let withdrawToken (self:etherdelta_cel_address) (sender:address{sender <> null}) (value:uint) (tx:tx) (block:block) (tokenId:uint) (token:address) (amount:uint)
: Eth1 unit
  (fun bst ->
    etherdelta_cel_live self bst /\ (
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
      (eth_balances2 self bst)
      /\ (eth_balances self bst)
  ))
  (fun bst ->
    let cs = CM.sel self bst.cmap in
    let b = pure_get_balance_bst self bst in
    let l = bst.log in
    ((tokenId == 0) \/ ((M.sel (M.sel cs.etherdelta_cel_tokens tokenId) sender) < amount))
    \/ (cs.etherdelta_cel__lock_)
  )
  (fun bst0 x bst1 ->
    etherdelta_cel_live self bst1 /\ (
    let cs0 = CM.sel self bst0.cmap in
    let cs1 = CM.sel self bst1.cmap in
    let b0 = pure_get_balance_bst self bst0 in
    let b1 = pure_get_balance_bst self bst1 in
    let l0 = bst0.log in
    let l1 = bst1.log in
    (eth_balances2 self bst1)
      /\ (eth_balances self bst1)
      /\ ((cs1.etherdelta_cel_tokenTxStatus ==> (M.equal cs1.etherdelta_cel_tokens (
        let x1 = (cs0.etherdelta_cel_tokens) in
        let x2 = (tokenId) in
        let x3 = (
          let x1 = (M.sel cs0.etherdelta_cel_tokens tokenId) in
          let x2 = (sender) in
          let x3 = ((M.sel (M.sel cs0.etherdelta_cel_tokens tokenId) sender) - amount) in
          let x1 = (M.upd x1 x2 x3) in
          x1) in
        let x1 = (M.upd x1 x2 x3) in
        x1))))
      /\ (cs0.etherdelta_cel_feeTake == cs1.etherdelta_cel_feeTake)
      /\ (cs0.etherdelta_cel_feeRebate == cs1.etherdelta_cel_feeRebate)
      /\ (cs0.etherdelta_cel_feeAccount == cs1.etherdelta_cel_feeAccount)
      /\ (cs0.etherdelta_cel_feeMake == cs1.etherdelta_cel_feeMake)
      /\ (cs0.etherdelta_cel_admin == cs1.etherdelta_cel_admin)
  ))
=
let cs = get_contract self in
let balance = get_balance self in
let _ = (if (cs.etherdelta_cel__lock_) then begin
revert "Reentrancy detected";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let x1 = ((tokenId = 0) || ((M.sel (M.sel cs.etherdelta_cel_tokens tokenId) sender) < amount)) in
let _ = (if x1 then begin
revert "Invalid token type or overflow";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let _ = etherdelta_cel_set__lock_ self true in
let cs = get_contract self in
assert ((cs.etherdelta_cel__lock_));
let x1 = unknown_call self in
let _ = etherdelta_cel_set_tokenTxStatus self (x1) in
let balance = get_balance self in
let _ = etherdelta_cel_set__lock_ self false in
let cs = get_contract self in
let _ = (if (cs.etherdelta_cel_totalBalance > balance) then begin
revert "Unexpected Ether transferred to self";
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
let x1 = (balance) in
let _ = etherdelta_cel_set_totalBalance self x1 in
let cs = get_contract self in
let x1 = (cs.etherdelta_cel_tokenTxStatus = true) in
let _ = (if x1 then begin
let x5 = ((_sub (M.sel (M.sel cs.etherdelta_cel_tokens tokenId) sender) amount)) in
let x4 = cs.etherdelta_cel_tokens in

let x3 = (tokenId) in
let x2 = (M.sel x4 x3) in
let x1 = (sender) in
let x0 = (M.sel x2 x1) in
  
let _ = etherdelta_cel_set_tokens self (M.upd x4 x3 (M.upd x2 x1 x5)) in
let cs = get_contract self in
let _ = emit etherdelta_cel_Withdraw (sender, token, amount, M.sel (M.sel cs.etherdelta_cel_tokens tokenId) sender) in
let cs = get_contract self in
let balance = get_balance self in
() end
else ()) in
let cs = get_contract self in
let balance = get_balance self in
()