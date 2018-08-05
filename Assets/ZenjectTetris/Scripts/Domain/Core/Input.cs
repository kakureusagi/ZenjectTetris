namespace ZenjectTetris.Domain.Core {

	/// <summary>
	/// ユーザーの入力.
	/// </summary>
	class Input : IInput {

		public InputType InputType { get; private set; }

		public MoveAmount MoveAmount { get; private set; }

		public void Set(InputType input, MoveAmount move) {
			InputType = input;
			MoveAmount = move;
		}

	}

}