namespace ZenjectTetris.Domain.Core {

	/// <summary>
	/// ユーザーの入力.
	/// </summary>
	interface IInput {

		/// <summary>
		/// 入力の種類.
		/// </summary>
		InputType InputType { get; }

		/// <summary>
		/// 移動量.
		/// </summary>
		MoveAmount MoveAmount { get; }

	}

}