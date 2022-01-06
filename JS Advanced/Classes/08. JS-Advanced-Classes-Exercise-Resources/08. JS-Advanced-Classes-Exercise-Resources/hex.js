class Hex {
	constructor (n) {
		this._value = n
	}
	valueOf () { return this._value }
	plus (number) {
		let result = this._value + Number(number.valueOf())

		return new Hex(result)
	}
	minus (number) {
		let result = this._value - Number(number.valueOf())

		return new Hex(result)
	}
	toString () {
		return `0x${(this._value.toString(16)).toUpperCase()}`
	}

	parse (text) {
		return parseInt(text, 16)
	}
}