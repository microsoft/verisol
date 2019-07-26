pragma solidity ^0.5.0;

import "./IERC20.sol";
import "./SafeMath.sol";
import "./VeriSolContracts.sol";

//import "../../math/SafeMath.sol";

/**
 * A highly simplified Token to express basic specifications
 * 
 * - totalSupply() equals the Sum({balanceOf(a) | a is an address })
 * - allowance[a][b] <= balanceOf(a)
 * 
 */
contract ERC20 is IERC20 {
    // using SafeMath for uint256;

    mapping (address => uint256) private _balances;

    mapping (address => mapping (address => uint256)) private _allowances;

    uint256 private _totalSupply;


    /**
     * A dummy constructor
     */
    constructor (uint256 totalSupply) public {
       require(msg.sender != address(0));
       _totalSupply = totalSupply;
       _balances[msg.sender] = totalSupply;
    }

    /**
     * @dev See {IERC20-totalSupply}.
     */
    function totalSupply() public view returns (uint256) {
        return _totalSupply;
    }

    /**
     * @dev See {IERC20-balanceOf}.
     */
    function balanceOf(address account) public view returns (uint256) {
        return _balances[account];
    }

    /**
     * @dev See {IERC20-transfer}.
     *
     * Requirements:
     *
     * - `recipient` cannot be the zero address.
     * - the caller must have a balance of at least `amount`.
     */
    function transfer(address recipient, uint256 amount) public returns (bool) {

        uint old_balance_sender = _balances[msg.sender];
        uint old_balance_recipient = _balances[recipient];

        _transfer(msg.sender, recipient, amount); 

        assert (
                (msg.sender == recipient && _balances[msg.sender] == old_balance_sender && _balances[recipient] == old_balance_recipient) ||
                (msg.sender != recipient && _balances[msg.sender] == old_balance_sender - amount && _balances[recipient] == old_balance_recipient + amount)
               );
        // weaker spec without the aliasing
        assert (old_balance_sender + old_balance_recipient == _balances[msg.sender] + _balances[recipient]);

        return true;
    }

    /**
     * @dev Moves tokens `amount` from `sender` to `recipient`.
     *
     * This is internal function is equivalent to {transfer}, and can be used to
     * e.g. implement automatic token fees, slashing mechanisms, etc.
     *
     * Emits a {Transfer} event.
     *
     * Requirements:
     *
     * - `sender` cannot be the zero address.
     * - `recipient` cannot be the zero address.
     * - `sender` must have a balance of at least `amount`.
     */
    function _transfer(address sender, address recipient, uint256 amount) internal {
        require(sender != address(0), "ERC20: transfer from the zero address");
        require(recipient != address(0), "ERC20: transfer to the zero address");
        require(_balances[sender] >= amount);

        _balances[sender] = SafeMath.sub(_balances[sender], amount);
        _balances[recipient] = SafeMath.add(_balances[recipient], amount);
        // emit Transfer(sender, recipient, amount); //issue #154 in VeriSol
    }


    function contractInvariant() private view {
        VeriSol.ContractInvariant(_totalSupply == VeriSol.SumMapping(_balances));
    }
}

