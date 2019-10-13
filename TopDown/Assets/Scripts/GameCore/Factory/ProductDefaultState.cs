using System;
using System.Collections.Generic;
using Character;
using GameCore.StateMachine;

namespace GameCore.Factory
{
    class ProductDefaultState : BaseProduct<AbstractCharacter>
    {
        public Func<object> CreateFunc;

        public ProductDefaultState(AbstractCharacter owner) : base(owner)
        {
            CreateFunc += CreateProduct;
        }

        public override object CreateProduct()
        {
            var newDictionary = new Dictionary<Type, State<AbstractCharacter>>
            {
                { typeof(Idle), new Idle(owner) },
                { typeof(Move), new Move(owner) },
                { typeof(Attack), new Attack(owner) },
            };
            return newDictionary;
        }
    }
}
