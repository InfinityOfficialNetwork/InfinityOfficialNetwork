
/*
 *    MCreator note: This file will be REGENERATED on each build.
 */
package net.infinityofficial.test_mod.init;

import net.minecraftforge.registries.RegistryObject;
import net.minecraftforge.registries.ForgeRegistries;
import net.minecraftforge.registries.DeferredRegister;

import net.minecraft.world.level.block.Block;

import net.infinityofficial.test_mod.block.CustomBlock1Block;
import net.infinityofficial.test_mod.TestModMod;

public class TestModModBlocks {
	public static final DeferredRegister<Block> REGISTRY = DeferredRegister.create(ForgeRegistries.BLOCKS, TestModMod.MODID);
	public static final RegistryObject<Block> CUSTOM_BLOCK_1 = REGISTRY.register("custom_block_1", () -> new CustomBlock1Block());
}
