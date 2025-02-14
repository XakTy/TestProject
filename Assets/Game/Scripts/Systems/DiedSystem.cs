﻿using Game.Scripts.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
	public sealed class DiedSystem : IEcsRunSystem
	{
		private readonly EcsFilter<CharacterTag, TransformRef, AnimatorRef, RendereRef, DiedEventZone> _filter;
		private readonly StaticData _staticData;

		public void Run()
		{
			foreach (var i in _filter)
			{
				var transform = _filter.Get2(i).value;
				transform.SetParent(null);

				var animator = _filter.Get3(i).value;
				animator.speed = 0f;

				var render = _filter.Get4(i).value;
				var materials = render.materials;
				materials[0] = _staticData.DiedMaterial;
				render.materials = materials;


				Object.Instantiate(_staticData.DiedParticle, transform.position, Quaternion.identity);

				var entity = _filter.GetEntity(i);
				entity.Del<CharacterTag>();
				entity.Del<DiedEventZone>();
			}
		}
	}
}