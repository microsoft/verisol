pragma solidity >=0.4.24<0.6.0;

/**
 * Library for VeriSol Code Contracts
 * 
 * Mechanism to add various specification constructs
 *   loop invariants
 *   contract invariants
 *   precondition
 *   postcondition
 *   
 * Also support enriching the assertion language 
 * 
 */
library VeriSol {

   /*
    ********************************************************************
    *  Specification mechanisms
    ********************************************************************
    */

    /**
     * Loop invariant
     *
     * Calling this function within a loop VeriSol.Invariant(I) installs
     * I as a loop invariant. I should only refer to variables in scope
     * at the loop entry, and evaluated at the loop head. 
     * 
     * Using "Invariant" to avoid clash with a potential "invariant" keyword in Solidity
     * to directly support loop invariants https://github.com/ethereum/solidity/issues/6210
     */
    function Invariant(bool b) external pure;
     
    
    /**
     * Contract invariant (stronger)
     * 
     * 
     * Calling this function within exactly one view function 
     * VeriSol.ContractInvariant(I) installs I  as (i) a loop invariant
     * for the harness that calls public methods in the contract in a 
     * loop. See https://arxiv.org/abs/1812.08829 (Sec IV A), and also
     * (ii) assume for every public method entry, and (iii) ensures for 
     * every public method, and (iv) before any external method call.
     *
     * It is currently not inherited by derived contracts
     *
     * I should only refer to variables in global scope i.e. state variables.
     * 
     */
    function ContractInvariant(bool b) external pure;


    /**
     * Contract invariant (weaker)
     * 
     * 
     * Calling this function within exactly one view function 
     * VeriSol.ContractInvariant(I, false) installs I  as a loop invariant
     * for the harness that calls public methods in the contract in a 
     * loop. See https://arxiv.org/abs/1812.08829 (Sec IV A)
     *
     * ContractInvariant(p, true) is identical to ContractInvariiant(p)
     * 
     * It is currently not inherited by derived contracts
     *
     * I should only refer to variables in global scope i.e. state variables.
     * 
     */
    function ContractInvariant(bool b, bool flag) external pure;

   /*
    ********************************************************************
    *  New functions for extending assertion language
    ********************************************************************
    */

    /**
     * A new in-built function that returns the sum of all values of a mapping
     * 
     */  
    function SumMapping(mapping (address => uint256) storage a) external pure returns (uint256);

    /**
     * Function to refer to the state of an expression at the entry to a method 
     * 
     */  
    function Old(uint x) external pure returns (uint);

    function Old(address x) external pure returns (address);

}