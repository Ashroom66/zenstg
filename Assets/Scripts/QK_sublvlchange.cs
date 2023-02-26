using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QK_sublvlchange : MonoBehaviour {
	private Player player;
	private SubEquip equip;
	private Subcon subcon;
	private int nowequip;
	private string subname;
	private int nowlvl;
	
	void Start () {
		player = GameObject.Find("Player").GetComponent<Player>();
		equip = player.GetComponent<SubEquip>();
		subcon = player.GetComponent<Subcon>();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
			// レベルチェンジ
			nowequip = equip.nowequip;
			if (nowequip == 1) {
				subname = equip.subname1;
				nowlvl = equip.substate1;
			} else if (nowequip == 2) {
				subname = equip.subname2;
				nowlvl = equip.substate2;
			} else if (nowequip == 3) {
				subname = equip.subname3;
				nowlvl = equip.substate3;
			} else if (nowequip == 4) {
				subname = equip.subname4;
				nowlvl = equip.substate4;
			} else if (nowequip == 5) {
				subname = equip.subname5;
				nowlvl = equip.substate5;
			}

			if (subname == "speed1") {
				if (nowlvl == 1) {
					subcon.speed1.gauge = subcon.speed1.cond2;
				} else if (nowlvl == 2) {
					subcon.speed1.gauge = subcon.speed1.cond3;
				} else {
					subcon.speed1.gauge = 0;
				}
			} else if (subname == "speed2") {
				if (nowlvl == 1) {
					subcon.speed2.gauge = subcon.speed2.cond2;
				} else if (nowlvl == 2) {
					subcon.speed2.gauge = subcon.speed2.cond3;
				} else {
					subcon.speed2.gauge = 0;
				}
			} else if (subname == "big1") {
				if (nowlvl == 1) {
					subcon.big1.gauge = subcon.big1.cond2;
				} else if (nowlvl == 2) {
					subcon.big1.gauge = subcon.big1.cond3;
				} else {
					subcon.big1.gauge = 0;
				}
			} else if (subname == "big2") {
				if (nowlvl == 1) {
					subcon.big2.gauge = subcon.big2.cond2;
				} else if (nowlvl == 2) {
					subcon.big2.gauge = subcon.big2.cond3;
				} else {
					subcon.big2.gauge = 0;
				}
			} else if (subname == "way1") {
				if (nowlvl == 1) {
					subcon.way1.gauge = subcon.way1.cond2;
				} else if (nowlvl == 2) {
					subcon.way1.gauge = subcon.way1.cond3;
				} else {
					subcon.way1.gauge = 0;
				}
			} else if (subname == "way2") {
				if (nowlvl == 1) {
					subcon.way2.gauge = subcon.way2.cond2;
				} else if (nowlvl == 2) {
					subcon.way2.gauge = subcon.way2.cond3;
				} else {
					subcon.way2.gauge = 0;
				}
			} else if (subname == "pierce1") {
				if (nowlvl == 1) {
					subcon.pierce1.gauge = subcon.pierce1.cond2;
				} else if (nowlvl == 2) {
					subcon.pierce1.gauge = subcon.pierce1.cond3;
				} else {
					subcon.pierce1.gauge = 0;
				}
			} else if (subname == "pierce2") {
				if (nowlvl == 1) {
					subcon.pierce2.gauge = subcon.pierce2.cond2;
				} else if (nowlvl == 2) {
					subcon.pierce2.gauge = subcon.pierce2.cond3;
				} else {
					subcon.pierce2.gauge = 0;
				}
			} else if (subname == "homing1") {
				if (nowlvl == 1) {
					subcon.homing1.gauge = subcon.homing1.cond2;
				} else if (nowlvl == 2) {
					subcon.homing1.gauge = subcon.homing1.cond3;
				} else {
					subcon.homing1.gauge = 0;
				}
			}  else if (subname == "homing2") {
				if (nowlvl == 1) {
					subcon.homing2.gauge = subcon.homing2.cond2;
				} else if (nowlvl == 2) {
					subcon.homing2.gauge = subcon.homing2.cond3;
				} else {
					subcon.homing2.gauge = 0;
				}
			}  else if (subname == "charge") {
				if (nowlvl == 1) {
					subcon.charge.gauge = subcon.charge.cond2;
				} else if (nowlvl == 2) {
					subcon.charge.gauge = subcon.charge.cond3;
				} else {
					subcon.charge.gauge = 0;
				}
			} else if (subname == "hexashield") {
				if (nowlvl == 1) {
					subcon.hexashield.gauge = subcon.hexashield.cond2;
				} else if (nowlvl == 2) {
					subcon.hexashield.gauge = subcon.hexashield.cond3;
				} else {
					subcon.hexashield.gauge = 0;
				}
			} 
		}
	}
	
}
