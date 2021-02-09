using System;
using System.Linq;
using Arc.Core.Extensions;
using Arc.Core.Types;
using Arc.Database.Models.Characters;
using Arc.Database.Models.Characters.Extensions;
using Arc.Database.Models.Inventories;
using Arc.Network.Packets;
using Arc.Network.Packets.Extensions;
using DateTime = Arc.Network.Packets.Extensions.DateTime;

namespace Arc.Game.Characters.Encodings
{
	public static class CharacterEndoings
	{
		
		public static void EncodeAvatarData(this Character character, IPacket outPacket, byte worldID)
		{
			character.EncodeCharacterStats(outPacket, worldID);

			outPacket.Encode<int>(0);
			outPacket.Encode<int>(0);
			outPacket.Encode<long>(0);

			character.EncodeAvatarLook(outPacket);

			//if isZero => Encode
		}

		public static void EncodeCharacterStats(this Character character, IPacket outPacket, byte worldID)
		{

			outPacket.Encode<int>(character.ID);
			outPacket.Encode<int>(character.ID);
			outPacket.Encode<int>(worldID);
			outPacket.Encode<string>(character.Name, 13);
			outPacket.Encode<byte>((byte)character.AvatarLook().Gender);
			outPacket.Encode<byte>(character.AvatarLook().Skin);
			outPacket.Encode<int>(character.AvatarLook().Face);
			outPacket.Encode<int>(character.AvatarLook().Hair);
			outPacket.Encode<byte>(0xFF);
			outPacket.Encode<byte>(0);
			outPacket.Encode<byte>(0);
			outPacket.Encode<int>(character.Level);// ?
			outPacket.Encode<short>(character.Job);
			outPacket.Encode<short>(character.Str);
			outPacket.Encode<short>(character.Dex);
			outPacket.Encode<short>(character.Int);
			outPacket.Encode<short>(character.Luk);
			outPacket.Encode<int>(character.HP);
			outPacket.Encode<int>(character.MaxHP);
			outPacket.Encode<int>(character.MP);
			outPacket.Encode<int>(character.MaxHP);
			outPacket.Encode<short>(character.AP);
			if (true)
			{
				int amount = 0;
				outPacket.Encode<byte>((byte)amount);
				for (int i = 1; i <= amount; i++)
				{
					outPacket.Encode<byte>((byte)i);
					outPacket.Encode<int>(0);
				}
			}
			else
			{
				outPacket.Encode<short>(0);
			}
			outPacket.Encode<long>(0);
			outPacket.Encode<int>(0);
			outPacket.Encode<int>(0); // Waru
			outPacket.Encode<int>(0);
			outPacket.Encode<int>(character.CurrentField);
			outPacket.Encode<byte>(character.CurrentPortal);
			outPacket.Encode<int>(0); // TODO figure out
			outPacket.Encode<short>(character.SubJob);
			if (false)
			{
				outPacket.Encode<int>(0);
			}
			outPacket.Encode<byte>(0);
			outPacket.Encode<System.DateTime>(System.DateTime.FromFileTimeUtc(132471072000000000));
			outPacket.Encode<short>(0);
			outPacket.Encode<int>(0);
			outPacket.Encode<int>(0);
			outPacket.Encode<int>(0);
			outPacket.Encode<int>(0);
			outPacket.Encode<int>(0);
			outPacket.Encode<int>(0);
			outPacket.Encode<int>(0);
			//getNonCombatStatDayLimit().encode(outPacket);

			outPacket.Encode<short>(0);
			outPacket.Encode<short>(0);
			outPacket.Encode<short>(0);
			outPacket.Encode<short>(0);
			outPacket.Encode<short>(0);
			outPacket.Encode<short>(0);
			outPacket.Encode<byte>(0);
			outPacket.Encode<System.DateTime>(DateTime.ZeroTime);


			outPacket.Encode<int>(0);
			outPacket.Encode<byte>(0);
			outPacket.Encode<int>(0);
			outPacket.Encode<byte>(2);


			outPacket.Encode<byte>(0);
			outPacket.Encode<int>(0);

			outPacket.Encode<System.DateTime>(DateTime.ZeroTime);

			outPacket.Encode<long>(0);
			outPacket.Encode<long>(0);
			outPacket.Encode<int>(0);
			outPacket.Encode<int>(0);
			outPacket.Encode<int>(0);
			outPacket.Encode<byte>(0);
			outPacket.Encode<int>(0);
			outPacket.Encode<int>(0);
			/*outPacket.Encode<byte>(getAlbaActivityID()); // part time job
            outPacket.encodeFT(getAlbaStartTime());
            outPacket.Encode<int>(getAlbaDuration());
            outPacket.Encode<byte>(getAlbaSpecialRewagrd());
            //getCharacterCard().encode(outPacket);
            outPacket.Encode<DateTime>(DateTime.UtcNow);
            // sub_939540
            outPacket.Encode<long>(0);
            outPacket.Encode<long>(0);
            outPacket.Encode<int>(0);
            outPacket.Encode<int>(0);
            outPacket.Encode<int>(0);
            outPacket.Encode<byte>(0);
            outPacket.Encode<int>(0);
            outPacket.Encode<int>(0);
            // end sub_939540*/
			outPacket.Encode<bool>(false); // bBurning

		}

		public static void EncodeAvatarLook(this Character character, IPacket outPacket)
		{

			outPacket.Encode<byte>((byte)character.AvatarLook().Gender);
			outPacket.Encode<byte>(character.AvatarLook().Skin);
			outPacket.Encode<int>(character.AvatarLook().Face);
			outPacket.Encode<int>(character.Job);
			outPacket.Encode<byte>(0); // ignored
			outPacket.Encode<int>(character.AvatarLook().Hair);
			/*
        for (int itemId : getHairEquips()) {
            int bodyPart = ItemConstants.getBodyPartFromItem(itemId, getGender());
            if (bodyPart != 0) {
                outPacket.Encode<byte>(bodyPart); // body part
                outPacket.Encode<int>(itemId); // item id
            }
        }*/
			/*
            outPacket.Encode<byte>(5);
            outPacket.Encode<int>(1053526);

            outPacket.Encode<byte>(7);
            outPacket.Encode<int>(1073414);

            outPacket.Encode<byte>(11);
            outPacket.Encode<int>(1213029);
            */
			outPacket.Encode<byte>(0xFF);
			/*
        for (int itemId : getUnseenEquips()) {
            outPacket.Encode<byte>(ItemConstants.getBodyPartFromItem(itemId, getGender())); // body part
            outPacket.Encode<int>(itemId);
        }*/
			outPacket.Encode<byte>(0xFF);

			outPacket.Encode<byte>(0xFF);
			/*
        for (int itemId : getTotems()) {
            outPacket.Encode<byte>(ItemConstants.getBodyPartFromItem(itemId, getGender()));
            outPacket.Encode<int>(itemId);
        }*/
			outPacket.Encode<byte>(0xFF);

			outPacket.Encode<int>(0);
			outPacket.Encode<int>(0);
			outPacket.Encode<int>(0);
			outPacket.Encode<int>((int)character.AvatarLook().EarType);
			outPacket.Encode<int>(0);
			outPacket.Encode<byte>(0);
			/*
        for (int i = 0; i < 3; i++) {
            if (getPetIDs().size() > i) {
                outPacket.Encode<int>(getPetIDs().get(i)); // always 3
            } else {*/
			outPacket.Encode<int>(0);
			outPacket.Encode<int>(0);
			outPacket.Encode<int>(0);
			/*
            }
        }*/
			/*
        short jobID = (short) getJob();
        if (JobConstants.isZero(jobID)) {
             outPacket.Encode<byte>(isZeroBetaLook());
        }
        if (JobConstants.isXenon(jobID)) {
            outPacket.Encode<int>(getXenonDefFaceAcc());
        }
        if  (JobConstants.isHoyoung(jobID)) {
            outPacket.Encode<int>(getHoyoungDefFaceAcc());
        }
        if (JobConstants.isDemon(jobID)) {
            outPacket.Encode<int>(getDemonSlayerDefFaceAcc());
        }
        if (JobConstants.isArk(jobID)) {
            outPacket.Encode<int>(getDemonSlayerDefFaceAcc());
        }
        if (JobConstants.isBeastTamer((short) getJob())) {
            bool hasEars = getEars() > 0;
            bool hasTail = getTail() > 0;
            outPacket.Encode<int>(getBeastTamerDefFaceAcc());
            outPacket.Encode<byte>(hasEars);
            outPacket.Encode<int>(getEars());
            outPacket.Encode<byte>(hasTail);
            outPacket.Encode<int>(getTail());
        }*/
			outPacket.Encode<byte>(0);
			outPacket.Encode<byte>(0);
			outPacket.Encode<int>(0);

		}

		public static void EncodeData(this Character character, IPacket outPacket, CharacterFields mask = CharacterFields.All)
		{
			outPacket.Encode<long>((long)mask);

			outPacket.Encode<byte>(0);  //CombatOrders

			for (int i = 0; i < 3; i++) // 3 being max pet amount
			{
				outPacket.Encode<int>(-1);

			}

			outPacket.Encode<byte>(0);

			{
				byte size = 0;
				outPacket.Encode<byte>(size);
				for (int i = 0; i < size; i++)
				{
					outPacket.Encode<int>(0);
				}
			}

			{
				int size = 0;
				outPacket.Encode<int>(size);
				for (int i = 0; i < size; i++)
				{
					outPacket.Encode<int>(0); // nKey
					outPacket.Encode<long>(0); // pInfo
				}
			}
			// -----------------------------------------

			// -----------------------------------------
			/*	TODO [HEX]
				  v15 = CInPacket::Decode1(inPacket);
				  v562 = 0;
				  v599 = 0;
				  if ( v15 )
				  {
					CInPacket::Decode1(inPacket);
					v16 = CInPacket::Decode4(inPacket);
					v549 = v16;
					if ( v16 > 0 )
					{
					  v17 = v16;
					  do
					  {
						CInPacket::DecodeBuffer(inPacket, &v460, 8);
						v18 = sub_971370(-1);
						*v18 = v460;
						v18[1] = v461;
						--v17;
					  }
					  while ( v17 );
					  v9 = v585;
					}
					v19 = CInPacket::Decode4(inPacket);
					v549 = v19;
					if ( v19 > 0 )
					{
					  v20 = v19;
					  do
					  {
						CInPacket::DecodeBuffer(inPacket, &v406, 8);
						--v20;
					  }
					  while ( v20 );
					  v9 = v585;
					}
				  }
			 */
			outPacket.Encode<byte>(0);

			if (mask.HasFlag(CharacterFields.Character))
			{
				//getAvatarData().getCharacterStat().encode(outPacket);
				character.EncodeCharacterStats(outPacket, 0);

				outPacket.Encode<byte>(0);

				bool hasBlessingOfFairy = false;
				outPacket.Encode<bool>(hasBlessingOfFairy);
				if (hasBlessingOfFairy)
				{
					outPacket.Encode<string>("");
				}

				bool hasBlessingOfEmpress = false;
				outPacket.Encode<bool>(hasBlessingOfEmpress);
				if (hasBlessingOfEmpress)
				{
					outPacket.Encode<string>("");
				}

				outPacket.Encode<bool>(false); // ultimate explorer, deprecated
			}

			if (mask.HasFlag(CharacterFields.Money))
			{
				outPacket.Encode<long>(0);
			}

			if (mask.HasFlag(CharacterFields.ConsumeItems) || mask.HasFlag(CharacterFields.ExpConsumeItems))
			{
				outPacket.Encode<int>(0);
				/*
				for (ExpConsumeItem eci : getExpConsumeItems())
				{
					eci.encode(outPacket);
				}
				*/
			}

			if (mask.HasFlag(CharacterFields.ConsumeItems) || mask.HasFlag(CharacterFields.ShopBuyLimit))
			{
				int size = 0;
				outPacket.Encode<int>(size);
				for (int i = 0; i < size; i++)
				{
					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);
					outPacket.Encode<long>(0);
					outPacket.Encode<long>(0);
				}
			}

			if (mask.HasFlag(CharacterFields.InventorySize))
			{
				outPacket.Encode<int>(character.GetInventorySlotAmount(InventoryType.Equip));
				outPacket.Encode<int>(character.GetInventorySlotAmount(InventoryType.Consume));
				outPacket.Encode<int>(character.GetInventorySlotAmount(InventoryType.Etc));
				outPacket.Encode<int>(character.GetInventorySlotAmount(InventoryType.Install));
				outPacket.Encode<int>(character.GetInventorySlotAmount(InventoryType.Cash));
				outPacket.Encode<int>(character.GetInventorySlotAmount(InventoryType.Decoration));
			}
			
			if (mask.HasFlag(CharacterFields.EquipExtension))
			{
				outPacket.Encode<int>(0); // ???
				outPacket.Encode<int>(0);
			}

			if (mask.HasFlag(CharacterFields.EquipItems))
			{
				bool skipEquipInvEncode = false;
				outPacket.Encode<bool>(skipEquipInvEncode);
				/*
				List<Item> equippedItems = new ArrayList<>(getEquippedInventory().getItems());
				equippedItems.sort(Comparator.comparingInt(Item::getBagIndex));

				*/
				var equippedInventory = character.GetEquippedInventory().Where(equip => equip.BagIndex >= (short)BodyPart.BPBase && equip.BagIndex < (short)BodyPart.BPEnd);


				foreach(var equip in equippedInventory)
                {
					outPacket.Encode<short>((short)equip.BagIndex); // TODO: change bag index to a short
					equip.Encode(outPacket);
                }
				/*
				// Normal equipped items
				for (Item item : equippedItems)
				{
					Equip equip = (Equip)item;
					outPacket.Encode<short>(17);
					outPacket.Encode<byte[]>("01 D0 1F 11 00 00 00 80 05 BB 46 E6 17 02 FF FF FF FF 00 FD 04 00 02 03 38 00 42 00 42 00 38 00 B4 00 54 06 BC 00 08 00 1C 00 00 00 FF B0 B9 01 00 00 00 00 00 00 11 00 00 00 00 12 0C 4C 4E AC 27 13 27 00 00 00 00 00 00 00 00 00 00 FF FF FF FF FF FF 00 00 00 00 C5 FB 05 00 E1 01 00 28 00 40 E0 FD 3B 37 4F 01 00 00 00 00 00 00 00 00 00 40 E0 FD 3B 37 4F 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF 00 80 05 BB 46 E6 17 02 00 40 E0 FD 3B 37 4F 01 00 80 05 BB 46 E6 17 02");
					if (item.getBagIndex() > BPBase.getVal() && item.getBagIndex() < BPEnd.getVal())
					{
						outPacket.Encode<short>(equip.getBagIndex());
						equip.encode(outPacket);
					}
				}*/
				outPacket.Encode<short>(0);

				// Equip inventory
				if (!skipEquipInvEncode)
				{/*
					for (Item item : getEquipInventory().getItems())
					{
						Equip equip = (Equip)item;
						outPacket.Encode<short>(equip.getBagIndex());
						equip.encode(outPacket);
					}*/
					outPacket.Encode<short>(0);
				}
				/*
				// Dragon (Evan)
				for (Item item : getEquippedInventory().getItems())
				{
					Equip equip = (Equip)item;
					if (item.getBagIndex() >= EvanBase.getVal() && item.getBagIndex() < EvanEnd.getVal())
					{
						outPacket.Encode<short>(equip.getBagIndex());
						equip.encode(outPacket);
					}
				}*/
				outPacket.Encode<short>(0);// 1
				/*
				// Guessing pet consume items, could very well be wrong
				for (Item item : getEquippedInventory().getItems())
				{
					Equip equip = (Equip)item;
					if (item.getBagIndex() >= 200 && item.getBagIndex() <= 300)
					{
						outPacket.Encode<short>(equip.getBagIndex());
						equip.encode(outPacket);
					}
				}*/
				outPacket.Encode<short>(0);// 2
				/*
				// VirtualEquipInventory::Decode (Android)
				// >= 20k < 200024?
				for (Item item : getEquippedInventory().getItems())
				{
					Equip equip = (Equip)item;
					if (item.getBagIndex() >= MechBase.getVal() && item.getBagIndex() < MechEnd.getVal())
					{
						outPacket.Encode<short>(equip.getBagIndex());
						equip.encode(outPacket);
					}
				}*/
				outPacket.Encode<short>(0); // 3
				/*
				// DressUp (Angelic Buster)
				for (Item item : getEquippedInventory().getItems())
				{
					Equip equip = (Equip)item;
					if (item.getBagIndex() >= DUBase.getVal() && item.getBagIndex() < DUEnd.getVal())
					{
						outPacket.Encode<short>(equip.getBagIndex());
						equip.encode(outPacket);
					}
				}*/
				outPacket.Encode<short>(0); // 4
				/*
				// Arcane
				for (Item item : getEquippedInventory().getItems())
				{
					Equip equip = (Equip)item;
					if (item.getBagIndex() >= ASBase.getVal() && item.getBagIndex() < ASEnd.getVal())
					{
						outPacket.Encode<short>(equip.getBagIndex());
						equip.encode(outPacket);
					}
				}*/
				outPacket.Encode<short>(0);// 5
				/*
				// Totems
				for (Item item : getEquippedInventory().getItems())
				{
					Equip equip = (Equip)item;
					if (item.getBagIndex() >= TotemBase.getVal() && item.getBagIndex() < TotemEnd.getVal())
					{
						outPacket.Encode<short>(equip.getBagIndex());
						equip.encode(outPacket);
					}
				}*/
				outPacket.Encode<short>(0);// 6
				/*
				// FoxMan (Kanna)
				for (Item item : getEquippedInventory().getItems())
				{
					Equip equip = (Equip)item;
					if (item.getBagIndex() >= HakuStart.getVal() && item.getBagIndex() < HakuEnd.getVal())
					{
						outPacket.Encode<short>(equip.getBagIndex());
						equip.encode(outPacket);
					}
				}*/
				outPacket.Encode<short>(0);// 7

				outPacket.Encode<int>(0);// unk
			}

			if (mask.HasFlag(CharacterFields.InstallItems))
			{
				outPacket.Encode<short>(0);
				outPacket.Encode<short>(0);
			}

			if (mask.HasFlag(CharacterFields.RedLeafInfo))
			{
				// Decoration equipped items
				bool skipEquipInvEncode = false;
				outPacket.Encode<bool>(skipEquipInvEncode);/*
				for (Item item : getEquippedInventory().getItems())
				{
					Equip equip = (Equip)item;
					if (item.getBagIndex() >= CBPBase.getVal() && item.getBagIndex() <= CBPEnd.getVal())
					{
						outPacket.Encode<short>(equip.getBagIndex() - 100);
						equip.encode(outPacket);
					}
				}*/
				outPacket.Encode<short>(0);
				/*
				// Decoration
				for (Item item : getDecorationInventory().getItems())
				{
					Equip equip = (Equip)item;
					outPacket.Encode<short>(equip.getBagIndex());
					equip.encode(outPacket);
				}*/
				outPacket.Encode<short>(0);// 1
				/*
						// Android
						for (Item item : getEquippedInventory().getItems()) {
							Equip equip = (Equip) item;
							if (item.getBagIndex() >= APBase.getVal() && item.getBagIndex() <= APEnd.getVal()) {
								outPacket.Encode<short>(equip.getBagIndex());
								equip.encode(outPacket);
							}
						}
						outPacket.Encode<short>(0);// 1
				*/
				// ZeroCash2 
				/*
				for (Item item : getEquippedInventory().getItems())
				{
					Equip equip = (Equip)item;
					if (item.getBagIndex() >= ZeroBase.getVal() && item.getBagIndex() < ZeroEnd.getVal())
					{
						outPacket.Encode<short>(equip.getBagIndex());
						equip.encode(outPacket);
					}
				} */
				outPacket.Encode<short>(0);// 2
				/*
				// Bits
				for (Item item : getEquippedInventory().getItems())
				{
					Equip equip = (Equip)item;
					if (item.getBagIndex() >= BitsBase.getVal() && item.getBagIndex() < BitsEnd.getVal())
					{
						outPacket.Encode<short>(equip.getBagIndex());
						equip.encode(outPacket);
					}
				}*/
				outPacket.Encode<short>(0);// 3
				/*
				// MonsterBattle
				for (Item item : getEquippedInventory().getItems())
				{
					Equip equip = (Equip)item;
					if (item.getBagIndex() >= MBPBase.getVal() && item.getBagIndex() < MBPEnd.getVal())
					{
						outPacket.Encode<short>(equip.getBagIndex());
						equip.encode(outPacket);
					}
				}*/
				outPacket.Encode<short>(0);// 4
			}
			// All except equip
			if (mask.HasFlag(CharacterFields.ConsumeItems))
			{/*
				for (Item item : getConsumeInventory().getItems())
				{
					outPacket.Encode<short>(item.getBagIndex());
					item.encode(outPacket);
				}*/
				outPacket.Encode<short>(0);
			}

			if (mask.HasFlag(CharacterFields.InstallItems))
			{/*
				for (Item item : getInstallInventory().getItems())
				{
					outPacket.Encode<short>(item.getBagIndex());
					item.encode(outPacket);
				}*/
				outPacket.Encode<short>(0);
			}

			if (mask.HasFlag(CharacterFields.EtcItems))
			{/*
				for (Item item : getEtcInventory().getItems())
				{
					outPacket.Encode<short>(item.getBagIndex());
					item.encode(outPacket);
				}*/
				outPacket.Encode<short>(0);
			}

			if (mask.HasFlag(CharacterFields.CashItems))
			{/*
				for (Item item : getCashInventory().getItems())
				{
					outPacket.Encode<short>(item.getBagIndex());
					item.encode(outPacket);
				}*/
				outPacket.Encode<short>(0);
			}
			// BagDatas all except equip
			if (mask.HasFlag(CharacterFields.ConsumeItems))
			{
				// TODO
				outPacket.Encode<int>(0);
			}

			if (mask.HasFlag(CharacterFields.InstallItems))
			{
				// TODO
				outPacket.Encode<int>(0);
			}

			if (mask.HasFlag(CharacterFields.EtcItems))
			{
				// TODO
				outPacket.Encode<int>(0);
			}
			// End bagdatas
			if (mask.HasFlag(CharacterFields.CoreAura))
			{
				int val = 0;
				outPacket.Encode<int>(val);
				for (int i = 0; i < val; i++)
				{
					outPacket.Encode<int>(0);
					outPacket.Encode<long>(0);
				}
			}

			if (mask.HasFlag(CharacterFields.UNK_0x40000000))
			{
				int val = 0;
				outPacket.Encode<int>(val);
				for (int i = 0; i < val; i++)
				{
					outPacket.Encode<long>(0);
					outPacket.Encode<long>(0);
				}
			}

			if (mask.HasFlag(CharacterFields.ItemPotRecord))
			{
				//bool hasItemPot = getItemPots() != null;
				bool hasItemPot = false;
				outPacket.Encode<bool>(hasItemPot);
				if (hasItemPot)
				{/*
					for (int i = 0; i < getItemPots().size(); i++)
					{
						getItemPots().get(i).encode(outPacket);
						outPacket.Encode<byte>(i != getItemPots().size() - 1);
					}*/
				}
			}

			if (mask.HasFlag(CharacterFields.SkillRecord))
			{
				bool encodeSkills = true;
				outPacket.Encode<bool>(encodeSkills);
				if (encodeSkills)
				{
					//Set<LinkSkill> linkSkills = getLinkSkills();
					//short size = (short)(getSkills().size() + linkSkills.size());
					short size = 0;
					outPacket.Encode<short>(size);
					/*
					for (Skill skill : getSkills())
					{
						outPacket.Encode<int>(skill.getSkillId());
						outPacket.Encode<int>(skill.getCurrentLevel());
						outPacket.encodeFT(FileTime.fromType(FileTime.Type.MAX_TIME));
						if (SkillConstants.isSkillNeedMasterLevel(skill.getSkillId()))
						{
							outPacket.Encode<int>(skill.getMasterLevel());
						}
					}
					for (LinkSkill linkSkill : linkSkills)
					{
						outPacket.Encode<int>(linkSkill.getLinkSkillID());
						outPacket.Encode<int>(linkSkill.getOwnerID());
						outPacket.encodeFT(FileTime.fromType(FileTime.Type.MAX_TIME));
						if (SkillConstants.isSkillNeedMasterLevel(linkSkill.getLinkSkillID()))
						{
							outPacket.Encode<int>(3); // whatevs
						}
					}*/
					//outPacket.Encode<short>(linkSkills.size());
					outPacket.Encode<short>(0);
					/*
					for (LinkSkill linkSkill : linkSkills)
					{
						outPacket.Encode<int>(linkSkill.getLinkSkillID()); // another nCount
						outPacket.Encode<short>(linkSkill.getLevel() - 1); // idk
					}
					/* TODO: [HEX]
						v584 = CInPacket::Decode4(v132);
		v547 = v165 + 4218;
		sub_973440(v165 + 4218);
		if ( v584 > 0 )
		{
		  do
		  {
			v171 = sub_988F00(0);
			v172 = (v171 + 20);
			*(v171 + 4) = 1;
			v494 = v171 + 20;
			LOBYTE(v599) = 23;
			sub_93ECD0((v171 + 20), v132);
			v551 = *v172;
			sub_98C810(v547, &v551, &v493);
			LOBYTE(v599) = 8;
			sub_978F10(0);
			v220 = v584-- == 1;
			v494 = 0;
		  }
		  while ( !v220 );
					 */
					outPacket.Encode<int>(0);
				}
				else
				{
					{
						short size = 0;
						outPacket.Encode<short>(size);
						for (int i = 0; i < size; i++)
						{
							outPacket.Encode<int>(0); // nTI
							outPacket.Encode<int>(0); // sValue
						}
					}

					{
						short size = 0;
						outPacket.Encode<short>(size);
						for (int i = 0; i < size; i++)
						{
							outPacket.Encode<int>(0); // nTI
						}
					}

					{
						short size = 0;
						outPacket.Encode<short>(size);
						for (int i = 0; i < size; i++)
						{
							outPacket.Encode<int>(0); // nTI
							outPacket.Encode<System.DateTime>(System.DateTime.MinValue); // pInfo
						}
					}

					{
						short size = 0;
						outPacket.Encode<short>(size);
						for (int i = 0; i < size; i++)
						{
							outPacket.Encode<int>(0); // nTI
						}
					}

                    {
						short size = 0;
						outPacket.Encode<short>(size);
						for (int i = 0; i < size; i++)
						{
							outPacket.Encode<int>(0); // nTI
							outPacket.Encode<int>(0); // sValue
						}
					}


					{
						short size = 0;
						outPacket.Encode<short>(size);
						for (int i = 0; i < size; i++)
						{
							outPacket.Encode<int>(0); // nTI
						}
					}
				}
			}

			if (mask.HasFlag(CharacterFields.SkillCooldown))
			{/*
				long curTime = System.currentTimeMillis();
				Map<Integer, Long> cooltimes = new HashMap<>();
				getSkillCoolTimes().forEach((key, value)-> {
					if (value - curTime > 0)
					{
						cooltimes.put(key, value);
					}
				});*/
				outPacket.Encode<short>(0);
				/*
				for (Map.Entry<Integer, Long> cooltime : cooltimes.entrySet())
				{
					outPacket.Encode<int>(cooltime.getKey()); // nSkillId
					outPacket.Encode<int>((int)((cooltime.getValue() - curTime) / 1000)); // nSkillCooltime
				}*/
			}

			if (mask.HasFlag(CharacterFields.QuestRecord))
			{
				// modified/deleted, not completed anyway
				bool removeAllOldEntries = true;
				outPacket.Encode<bool>(removeAllOldEntries);
				//short size = (short)getQuestManager().getQuestsInProgress().size();
				outPacket.Encode<short>(0);
				/*
				for (Quest quest : getQuestManager().getQuestsInProgress())
				{
					outPacket.Encode<int>(quest.getQRKey());
					outPacket.Encode<string>(quest.getQRValue());
				}
				if (!removeAllOldEntries)
				{
					// blacklisted quests
					short size2 = 0;
					outPacket.Encode<short>(size2);
					for (int i = 0; i < size2; i++)
					{
						outPacket.Encode<int>(0); // nQRKey
					}
				}*/
				short size = 0;
				outPacket.Encode<short>(size);
				// Not sure what this is for
				for (int i = 0; i < size; i++)
				{
					outPacket.Encode<string>("");
					outPacket.Encode<string>("");
				}
			}

			if (mask.HasFlag(CharacterFields.QuestComplete))
			{
				bool removeAllOldEntries = true;
				outPacket.Encode<bool>(removeAllOldEntries);
				//Set<Quest> completedQuests = getQuestManager().getCompletedQuests();
				outPacket.Encode<short>(0);
				/*
				for (Quest quest : completedQuests)
				{
					outPacket.Encode<int>(quest.getQRKey());
					outPacket.encodeFT(quest.getCompletedTime());
				}
				if (!removeAllOldEntries)
				{
					short size = 0;
					outPacket.Encode<short>(size);
					for (int i = 0; i < size; i++)
					{
						outPacket.Encode<int>(0); // nQRKey?
					}
				}*/
			}

			if (mask.HasFlag(CharacterFields.MinigameRecord))
			{
				short size = 0;
				outPacket.Encode<short>(size);
				for (int i = 0; i < size; i++)
				{
					//new MiniGameRecord().encode(outPacket);
				}
			}

			if (mask.HasFlag(CharacterFields.CoupleRecord))
			{
				short coupleSize = 0;
				outPacket.Encode<short>(coupleSize);
				for (int i = 0; i < coupleSize; i++)
				{
					//new CoupleRecord().encode(outPacket);
				}
				short friendSize = 0;
				outPacket.Encode<short>(friendSize);
				for (int i = 0; i < friendSize; i++)
				{
					//new FriendRecord().encode(outPacket);
				}
				short marriageSize = 0;
				outPacket.Encode<short>(marriageSize);
				for (int i = 0; i < marriageSize; i++)
				{
					//new MarriageRecord().encode(outPacket);
				}
			}

			if (mask.HasFlag(CharacterFields.MapTransfer))
			{
				for (int i = 0; i < 5; i++)
				{
					outPacket.Encode<int>(999999999);
				}
				for (int i = 0; i < 10; i++)
				{
					outPacket.Encode<int>(999999999);
				}
				for (int i = 0; i < 13; i++)
				{
					outPacket.Encode<int>(999999999);
				}
				for (int i = 0; i < 13; i++)
				{
					outPacket.Encode<int>(999999999);
				}
			}

			//if (mask.HasFlag(CharacterFields.MonsterBookCover)) {
			//	outPacket.Encode<int>(getMonsterBookInfo().getCoverID());
			//}

			if (mask.HasFlag(CharacterFields.FamiliarManagerInfo))
			{
				outPacket.Encode<int>(character.ID);
				outPacket.Encode<short>(2000);    // current summon gauge charge
				outPacket.Encode<short>(100); // UNK
				outPacket.Encode<byte[]>(new byte[6]);
				outPacket.Encode<System.DateTime>(System.DateTime.UtcNow);
				for (int j = 0; j < 3; j++)
				{
					outPacket.Encode<int>(0); //	selected familiars
				}
				for (int i = 0; i < 8; i++)
				{
					outPacket.Encode<byte>(0xFF);   //	selected badges
				}
			}
			/*
					if (mask.HasFlag(CharacterFields.MonsterBookCard)) {
						bool isCompleted = false;
						outPacket.Encode<byte>(isCompleted);
						if (!isCompleted) {
							short size = (short) getMonsterBookInfo().getCards().size();
							outPacket.Encode<short>(size);
							for (int card : getMonsterBookInfo().getCards()) {
								outPacket.Encode<short>(card);
								outPacket.Encode<byte>(true); // bEnabled?
							}
						} else {
							outPacket.Encode<short>(0); // card list size
							short encSize = 0;
							outPacket.Encode<short>(encSize);
							outPacket.Encode<byte[]>(new byte[encSize]);
							encSize = 0;
							outPacket.Encode<short>(encSize);
							outPacket.Encode<byte[]>(new byte[encSize]);
						}
						outPacket.Encode<int>(getMonsterBookInfo().getSetID()); // monsterbook set
					}
			*/
			if (mask.HasFlag(CharacterFields.FamiliarCollectionRecord))
			{
				outPacket.Encode<byte>(0);
				int size = 0;
				outPacket.Encode<int>(size);
				for (int i = 0; i < size; i++)
				{
					outPacket.Encode<int>(1);
					outPacket.Encode<int>(65535);
					outPacket.Encode<int>(character.ID);
					outPacket.Encode<byte[]>(new byte[46]);
				}


				outPacket.Encode<byte>(0);
				size = 0;
				outPacket.Encode<int>(size);
				for (int i = 0; i < size; i++)
				{
					outPacket.Encode<int>(i);
					outPacket.Encode<int>(character.ID);
					outPacket.Encode<short>((short)i);
					outPacket.Encode<byte[]>(new byte[16]);
				}
			}

			outPacket.Encode<byte[]>(new byte[102]);

			if (mask.HasFlag(CharacterFields.QuestRecordEx))
			{
				outPacket.Encode<short>(0);
				/*
				for (Map.Entry<Integer, QuestEx> questEx : getQuestRecordEx().entrySet())
				{
					outPacket.Encode<int>(questEx.getKey());
					String str = "";
					for (Map.Entry<String, String> value : questEx.getValue().getValues().entrySet())
					{
						if (str.isEmpty())
						{
							str = String.format("%s=%s", value.getKey(), value.getValue());
						}
						else
						{
							str = String.format("%s;%s=%s", str, value.getKey(), value.getValue());
						}
					}
					outPacket.Encode<string>(str);
				}*/
			}
			/*
					if (mask.HasFlag(CharacterFields.QuestCompleteOld)) {
						short size = 0;
						outPacket.Encode<short>(size);
						for (int i = 0; i < size; i++) {
							outPacket.Encode<short>(0);
						}
					}

					if (mask.HasFlag(CharacterFields.Familiar)) {
						outPacket.Encode<int>(getFamiliars().size());
						for (Familiar familiar : getFamiliars()) {
							familiar.encode(outPacket);
						}
					}

			*/
			if (mask.HasFlag(CharacterFields.UNK_0x200000000000))
			{
				short size = 0;
				outPacket.Encode<short>(size);
				for (int i = 0; i < size; i++)
				{
					outPacket.Encode<int>(0); // sValue
											  //new AvatarLook()()().encode(outPacket);
				}
			}

			if (mask.HasFlag(CharacterFields.UNK_0x80000))
			{
				short size = 0;
				outPacket.Encode<short>(size);
				for (int i = 0; i < size; i++)
				{
					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);
					outPacket.Encode<string>("");
					outPacket.Encode<byte>(0);
					outPacket.Encode<long>(0);
					outPacket.Encode<int>(0);
					outPacket.Encode<string>("");
					outPacket.Encode<byte>(0);
					outPacket.Encode<byte>(0);
					outPacket.Encode<long>(0);
					outPacket.Encode<string>("");
				}
			}

			if (mask.HasFlag(CharacterFields.UNK_0x10000000000))
			{
				outPacket.Encode<byte>(0);// v178
			}
			/*
		if (mask.HasFlag(CharacterFields.Unk3)) { //related to the byte above
			int size = 0;
			outPacket.Encode<int>(0);
			for (int i = 0; i < size; i++) {
				outPacket.Encode<int>(0);
				outPacket.Encode<string>("");
			}
		}
*/
			if (mask.HasFlag(CharacterFields.UNK_0x100000000000))
			{
				int size = 0;
				outPacket.Encode<int>(0);
				for (int i = 0; i < size; i++)
				{
					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);
				}
			}

			if (mask.HasFlag(CharacterFields.WildHunterInfo))
			{
				/*
				if (JobConstants.isWildHunter(getAvatarData().getCharacterStat().getJob()))
				{
					getWildHunterInfo().encode(outPacket); // GW_WildHunterInfo::Decode
				}*/
			}

			if (mask.HasFlag(CharacterFields.ZeroInfo))
			{/*
				if (JobConstants.isZero(getAvatarData().getCharacterStat().getJob()))
				{
					if (getZeroInfo() == null)
					{
						initZeroInfo();
					}
					getZeroInfo().encode(outPacket); // ZeroInfo::Decode
				}*/
			}

			if (mask.HasFlag(CharacterFields.ShopBuyLimit))
			{
				short size = 0;
				outPacket.Encode<short>(size);
				for (int i = 0; i < size; i++)
				{
					// Encode shop buy limit
				}
			}

			if (mask.HasFlag(CharacterFields.UNK_0x80000000000000))
			{
				short size = 0;
				outPacket.Encode<short>(size);
				for (int i = 0; i < size; i++)
				{
					// Encode something
				}
			}

			if (mask.HasFlag(CharacterFields.StolenSkills))
			{
				if (false)//(JobConstants.isPhantom(getAvatarData().getCharacterStat().getJob()))
				{
					for (int i = 0; i < 15; i++)
					{
						//StolenSkill stolenSkill = getStolenSkillByPosition(i);
						//outPacket.Encode<int>(stolenSkill == null ? 0 : stolenSkill.getSkillid());
					}
				}
				else
				{
					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);

					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);

					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);

					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);

					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);
				}
			}

			if (mask.HasFlag(CharacterFields.ChosenSkills))
			{
				if (false)//(JobConstants.isPhantom(getAvatarData().getCharacterStat().getJob()))
				{
					for (int i = 1; i <= 5; i++)
					{ //Shifted by +1 to accomodate the Skill Management Tabs
					  //ChosenSkill chosenSkill = getChosenSkillByPosition(i);
					  //outPacket.Encode<int>(chosenSkill == null
					  //		? 0
					  //		: isChosenSkillInStolenSkillList(chosenSkill.getSkillId())
					  //		? chosenSkill.getSkillId()
					  //		: 0
					  //);
					}
				}
				else
				{
					for (int i = 0; i < 5; i++)
					{
						outPacket.Encode<int>(0);
					}
				}
			}

			if (mask.HasFlag(CharacterFields.CharacterPotential))
			{
				outPacket.Encode<short>(0);/*
				for (CharacterPotential cp : getPotentials())
				{
					cp.encode(outPacket);
				}*/
			}

			if (mask.HasFlag(CharacterFields.SoulCollection))
			{
				short size = 0;
				outPacket.Encode<short>(size);
				for (int i = 0; i < size; i++)
				{
					outPacket.Encode<int>(0); //
					outPacket.Encode<int>(0); //
				}
			}

			{
				int sizee = 0;
				outPacket.Encode<int>(sizee);
				for (int i = 0; i < sizee; i++)
				{
					outPacket.Encode<string>("");
					// sub_73A1A0
					outPacket.Encode<int>(0);
					outPacket.Encode<string>("");
					int size = 0;
					outPacket.Encode<int>(size);
					for (int j = 0; j < size; j++)
					{
						outPacket.Encode<byte>(0);
					}
				}
			}
			outPacket.Encode<byte>(0); // idk

			if (mask.HasFlag(CharacterFields.Character))
			{
				outPacket.Encode<int>(0); // honor level, deprecated
				outPacket.Encode<int>(0); // honor exp
			}

			if (mask.HasFlag(CharacterFields.UNK_0x200000000))
			{
				bool shouldIEncodeThis = true;
				outPacket.Encode<bool>(shouldIEncodeThis);
				if (shouldIEncodeThis)
				{
					short size = 0;
					outPacket.Encode<short>(size);
					for (int i = 0; i < size; i++)
					{
						short category = 0;
						outPacket.Encode<short>(category);
						short size2 = 0;
						outPacket.Encode<short>(size2);
						for (int i2 = 0; i2 < size2; i2++)
						{
							outPacket.Encode<int>(0); // nItemId
							outPacket.Encode<int>(0); // nCount
						}
					}
				}
				else
				{
					short size2 = 0;
					outPacket.Encode<short>(size2);
					for (int i2 = 0; i2 < size2; i2++)
					{
						outPacket.Encode<short>(0); // nCategory
						outPacket.Encode<int>(0); // nItemId
						outPacket.Encode<int>(0); // nCount
					}

				}
			}

			if (mask.HasFlag(CharacterFields.ReturnEffectInfo))
			{
				//            getReturnEffectInfo().encode(outPacket); // ReturnEffectInfo::Decode
				outPacket.Encode<byte>(0);
			}

			if (mask.HasFlag(CharacterFields.DressUpInfo))
			{
				//new DressUpInfo().encode(outPacket); // GW_DressUpInfo::Decode
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<byte>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
			}

			if (mask.HasFlag(CharacterFields.ActiveDamageSkin))
			{
				outPacket.Encode<int>(-1);
				outPacket.Encode<int>(-1);
				outPacket.Encode<long>(0);
				outPacket.Encode<string>("");
				outPacket.Encode<int>(-1);
			}

			if (mask.HasFlag(CharacterFields.CoreInfo))
			{
				// GW_Core
				short size = 0;
				outPacket.Encode<short>(size);
				for (int i = 0; i < size; i++)
				{
					outPacket.Encode<short>(0); // nPos
					outPacket.Encode<int>(0); // nCoreID
					outPacket.Encode<int>(0); // nLeftCount
				}

				size = 0;
				outPacket.Encode<short>(size);
				for (int i = 0; i < size; i++)
				{
					outPacket.Encode<short>(0); // nPos
					outPacket.Encode<int>(0); // nCoreID
					outPacket.Encode<int>(0); // nLeftCount
				}
			}

			if (mask.HasFlag(CharacterFields.FarmPotential))
			{
				//new FarmPotential().encode(outPacket); // FARM_POTENTIAL::Decode
				int size = 0;
				outPacket.Encode<int>(size);
				for (int i = 0; i < size; i++)
				{
					outPacket.Encode<int>(0); // dwMonsterID
					outPacket.Encode<System.DateTime>(DateTime.MaxTime); // potentialExpire
				}
			}

			if (mask.HasFlag(CharacterFields.FarmUserInfo))
			{
				//new FarmUserInfo().encode(outPacket); // FarmUserInfo::Decode
				outPacket.Encode<string>("rip");
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
			}

			if (mask.HasFlag(CharacterFields.MemorialCubeInfo))
			{
				//new MemorialCubeInfo().encode(outPacket); // MemorialCubeInfo::Decode
				//Equip equip = getEquip();
				outPacket.Encode<bool>(false);
				/*
				if (equip != null)
				{
					equip.encode(outPacket);
					outPacket.Encode<int>(getCubeItemID());
					outPacket.Encode<int>(equip.getBagIndex());
				}*/
			}

			if (mask.HasFlag(CharacterFields.MemorialFlameInfo))
			{
				outPacket.Encode<long>(0);
				outPacket.Encode<long>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
			}

			if (mask.HasFlag(CharacterFields.LikePointRecord))
			{
				//new LikePoint().encode(outPacket);
				outPacket.Encode<int>(0);
				outPacket.Encode<System.DateTime>(DateTime.ZeroTime);
				outPacket.Encode<int>(0);
			}

			if (mask.HasFlag(CharacterFields.RunnerGameRecord))
			{
				//new RunnerGameRecord().encode(outPacket);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<System.DateTime>(DateTime.ZeroTime);
				outPacket.Encode<int>(0);
			}

			if (mask.HasFlag(CharacterFields.UNK_0x8000000000000))
			{
				// sub_92A4F0
				int size = 0;
				outPacket.Encode<int>(size);
				for (int i = 0; i < size; i++)
				{
					outPacket.Encode<int>(0);
					outPacket.Encode<byte>(0);
					outPacket.Encode<byte>(0);
					outPacket.Encode<byte>(0);
				}
				outPacket.Encode<int>(0);
				outPacket.Encode<long>(0);
			}

			short sizeO = 0;
			outPacket.Encode<short>(sizeO);
			for (int i = 0; i < sizeO; i++)
			{
				outPacket.Encode<int>(0);
				outPacket.Encode<string>("");

			}

			if (mask.HasFlag(CharacterFields.MonsterCollectionRecord))
			{
				//Set<MonsterCollectionExploration> mces = getAccount().getMonsterCollection().getMonsterCollectionExplorations();
				outPacket.Encode<short>(0);
				/*
				for (MonsterCollectionExploration mce : mces)
				{
					outPacket.Encode<int>(mce.getPosition());
					outPacket.Encode<string>(mce.getValue(true));
				}*/
			}

			{
				bool farmOnline = false;
				outPacket.Encode<bool>(farmOnline);
			}

			{
				int sizeInt = 0;
				// CharacterData::DecodeTextEquipInfo
				outPacket.Encode<int>(sizeInt);
				for (int i = 0; i < sizeInt; i++)
				{
					outPacket.Encode<int>(0);
					outPacket.Encode<string>("");
				}
			}

			if (mask.HasFlag(CharacterFields.UNK_0x10000000000000))
			{
				outPacket.Encode<short>(0);
				for (int i = 0; i < 0; i++)
				{
					outPacket.Encode<int>(0);
					outPacket.Encode<int>(0);
				}
			}

			if (mask.HasFlag(CharacterFields.VMatrixRecord))
			{
				//matrixInventory.encode(outPacket);
				outPacket.Encode<long>(0);
			}

			if (mask.HasFlag(CharacterFields.AchievementRecord))
			{
				// sub_92C4A0
				outPacket.Encode<int>(0);// acc id
				outPacket.Encode<int>(0);// char id

				//	sub_92B650
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<System.DateTime>(DateTime.ZeroTime);
				// end sub_92B650

				outPacket.Encode<long>(0);
				outPacket.Encode<long>(0);
				outPacket.Encode<int>(0);// 219
				outPacket.Encode<int>(0);// 1255
										 // end sub_92C4A0
			}

			if (mask.HasFlag(CharacterFields.EtcItems))
			{   // Boss Crystals in inventory
				outPacket.Encode<int>(0);
			}

			if (mask.HasFlag(CharacterFields.UNK_0x200000000000000))
			{
				outPacket.Encode<byte>(0);
				outPacket.Encode<byte>(0);
				for (int i = 0; i < 9; i++)
				{
					bool read = false;
					outPacket.Encode<bool>(read);
					if (read)
					{
						outPacket.Encode<byte>(0);
						outPacket.Encode<byte>(0);
						outPacket.Encode<int>(0);
						outPacket.Encode<byte>(0);
						outPacket.Encode<byte>(0);
						outPacket.Encode<byte>(0);
					}
				}
			}

			if (mask.HasFlag(CharacterFields.UNK_0x400000000000000))
			{
				outPacket.Encode<byte>(0);
				outPacket.Encode<byte>(0);
				for (int i = 0; i < 9; i++)
				{
					bool read = false;
					outPacket.Encode<bool>(read);
					if (read)
					{
						outPacket.Encode<byte>(0);
						outPacket.Encode<byte>(0);
						outPacket.Encode<int>(0);
						outPacket.Encode<byte>(0);
						outPacket.Encode<byte>(0);
						outPacket.Encode<byte>(0);
					}
				}
			}

			if (mask.HasFlag(CharacterFields.FamiliarCollectionRecord))
			{
				outPacket.Encode<byte>(0);
				outPacket.Encode<byte>(0);
				for (int i = 0; i < 9; i++)
				{
					bool read = false;
					outPacket.Encode<bool>(read);
					if (read)
					{
						outPacket.Encode<byte>(0);
						outPacket.Encode<byte>(0);
						outPacket.Encode<int>(0);
						outPacket.Encode<byte>(0);
						outPacket.Encode<byte>(0);
						outPacket.Encode<byte>(0);
					}
				}
			}

			if (mask.HasFlag(CharacterFields.EmoteRecord))
			{
				// Emotes
				int someSize4 = 0;
				outPacket.Encode<int>(someSize4);
				for (int i = 0; i < someSize4; i++)
				{
					outPacket.Encode<int>(0);
					outPacket.Encode<byte[]>(new byte[14]);
				}

				int someSize5 = 0;
				outPacket.Encode<int>(someSize5);
				for (int i = 0; i < someSize5; i++)
				{
					outPacket.Encode<short>(0);
					outPacket.Encode<int>(0);
				}

				outPacket.Encode<short>(5); //open slots ?/40

				int someSize6 = 0;
				outPacket.Encode<int>(someSize6);
				for (int i = 0; i < someSize6; i++)
				{
					outPacket.Encode<short>(0);
					outPacket.Encode<byte[]>(new byte[25]);
				}

				int someSize7 = 0;
				outPacket.Encode<int>(someSize7);
				for (int i = 0; i < someSize7; i++)
				{
					outPacket.Encode<string>("");
					outPacket.Encode<byte[]>(new byte[25]);
				}
			}


			if (mask.HasFlag(CharacterFields.UNK_0x4000000000000000))
			{
				// mushy
				outPacket.Encode<byte>(1);
				outPacket.Encode<byte>(0);
				outPacket.Encode<int>(1);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(100);
				outPacket.Encode<System.DateTime>(DateTime.MaxTime);
				outPacket.Encode<short>(0);
				outPacket.Encode<short>(0);
			}

			if (mask.HasFlag(CharacterFields.UNK_0x80000))
			{
				outPacket.Encode<byte>(0);
			}

			if (mask.HasFlag(CharacterFields.UNK_0x80000))
			{
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
			}

			if (mask.HasFlag(CharacterFields.UNK_0x2000000000000000))
			{ // 70 Bytes
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);

				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);

				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);

				outPacket.Encode<long>(0);
				outPacket.Encode<byte>(0);

				outPacket.Encode<byte>(1);
			}

			if (mask.HasFlag(CharacterFields.ActiveDamageSkin))
			{
				outPacket.Encode<short>(0);
				for (int i = 0; i < 0; i++)
				{
					outPacket.Encode<short>(0);
					outPacket.Encode<short>(0);
				}
			}

			if (mask.HasFlag(CharacterFields.RedLeafInfo))
			{
				// red leaf information
				outPacket.Encode<int>(character.ID);
				outPacket.Encode<int>(character.ID);
				outPacket.Encode<int>(4);
				outPacket.Encode<int>(0);
				outPacket.Encode<byte[]>(new byte[32]); // real
			}

			if (mask.HasFlag(CharacterFields.UNK_0x80000))
			{
				outPacket.Encode<byte>(0);// if true avatar look encode
			}

			if (mask.HasFlag(CharacterFields.UNK_0x80000))
			{
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<int>(0);
				outPacket.Encode<short>(0);
				outPacket.Encode<short>(0);
			}
		}
	}
}