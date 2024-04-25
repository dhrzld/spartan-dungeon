using System;
using System.Collections.Generic;

class SpartanDungeon
{
    static int level = 1;
    static string playerName = "Chad";
    static string playerClass = "전사";
    static int baseAttack = 10;
    static int baseDefense = 5;
    static int health = 100;
    static int gold = 800;
    static List<Item> inventory = new List<Item>();

    static void Main(string[] args)
    {
        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");

        Item learnerArmor = new Item("수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000);
        Item ironArmor = new Item("무쇠갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 500);
        Item spartaArmor = new Item("스파르타의 갑옷", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500);
        Item oldSword = new Item("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600);
        Item bronzeAxe = new Item("청동 도끼", 5, 0, "어디선가 사용됐던 거 같은 도끼입니다.", 1500);
        Item spartaSpear = new Item("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 1000);

        List<Item> shopItems = new List<Item> { learnerArmor, ironArmor, spartaArmor, oldSword, bronzeAxe, spartaSpear };

        ShowMainMenu(shopItems);
    }

    static void ShowMainMenu(List<Item> shopItems)
    {
        while (true)
        {
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("0. 종료");
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CharacterInfo();
                    break;
                case "2":
                    Inventory();
                    break;
                case "3":
                    Shop(shopItems);
                    break;
                case "0":
                    Console.WriteLine("게임을 종료합니다.");
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    break;
            }
        }
    }

    static void CharacterInfo()
    {
        Console.WriteLine($"Lv. {level}");
        Console.WriteLine($"{playerName} ( {playerClass} )");
        Console.WriteLine($"공격력 : {baseAttack}");
        Console.WriteLine($"방어력 : {baseDefense}");
        Console.WriteLine($"체 력 : {health}");
        Console.WriteLine($"Gold : {gold} G");

        int totalAttack = baseAttack;
        int totalDefense = baseDefense;

        foreach (Item item in inventory)
        {
            if (item.IsEquipped)
            {
                totalAttack += item.AttackBonus;
                totalDefense += item.DefenseBonus;
            }
        }

        Console.WriteLine($"총 공격력 : {totalAttack}");
        Console.WriteLine($"총 방어력 : {totalDefense}");
    }

    static void Inventory()
    {
        Console.WriteLine("인벤토리 - 장착/해제 관리");

        if (inventory.Count == 0)
        {
            Console.WriteLine("인벤토리가 비어 있습니다.");
            return;
        }

        Console.WriteLine("[아이템 목록]");

        for (int i = 0; i < inventory.Count; i++)
        {
            string equippedIndicator = inventory[i].IsEquipped ? " [E]" : ""; // 아이템이 장착되었을 때 [E] 표시 추가
            Console.WriteLine($"{i + 1}. {inventory[i]}{equippedIndicator}");
        }

        Console.WriteLine("아이템을 장착하거나 해제할 아이템의 번호를 입력하세요 (0: 나가기, -1: 판매하기):");
        int itemIndex;
        if (int.TryParse(Console.ReadLine(), out itemIndex))
        {
            if (itemIndex >= 1 && itemIndex <= inventory.Count)
            {
                Item selectedItem = inventory[itemIndex - 1];
                if (!selectedItem.IsEquipped)
                {
                    selectedItem.IsEquipped = true;
                    Console.WriteLine($"{selectedItem.Name}을(를) 장착하였습니다.");
                }
                else
                {
                    selectedItem.IsEquipped = false;
                    Console.WriteLine($"{selectedItem.Name}을(를) 해제하였습니다.");
                }
            }
            else if (itemIndex == 0)
            {
                return;
            }
            else if (itemIndex == -1)
            {
                SellItem();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
        }
    }

    static void Shop(List<Item> shopItems)
    {
        while (true)
        {
            Console.WriteLine("상점에 오신 것을 환영합니다.");
            Console.WriteLine($"[보유 골드] {gold} G");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < shopItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {shopItems[i]}");
            }

            Console.WriteLine("구매할 아이템의 번호를 입력하세요 (0: 나가기):");
            int itemIndex;
            if (int.TryParse(Console.ReadLine(), out itemIndex))
            {
                if (itemIndex >= 1 && itemIndex <= shopItems.Count)
                {
                    Item selectedItem = shopItems[itemIndex - 1];
                    if (gold >= selectedItem.Price)
                    {
                        gold -= selectedItem.Price;
                        inventory.Add(selectedItem);
                        Console.WriteLine($"{selectedItem.Name}을(를) 구매하였습니다.");
                    }
                    else
                    {
                        Console.WriteLine("골드가 부족합니다.");
                    }
                }
                else if (itemIndex == 0)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }

    static void SellItem()
    {
        Console.WriteLine("[판매할 아이템 목록]");
        for (int i = 0; i < inventory.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {inventory[i]}");
        }
        Console.WriteLine("판매할 아이템의 번호를 입력하세요 (0: 나가기):");
        int itemIndex;
        if (int.TryParse(Console.ReadLine(), out itemIndex))
        {
            if (itemIndex >= 1 && itemIndex <= inventory.Count)
            {
                Item selectedItem = inventory[itemIndex - 1];
                gold += (int)(selectedItem.Price * 0.85); // 85% 가격에 판매
                Console.WriteLine($"{selectedItem.Name}을(를) 판매하였습니다.");
                inventory.RemoveAt(itemIndex - 1); // 인벤토리에서 아이템 삭제
            }
            else if (itemIndex == 0)
            {
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
        }
    }
}

class Item
{
    public string Name { get; set; }
    public int AttackBonus { get; set; }
    public int DefenseBonus { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public bool IsEquipped { get; set; }

    public Item(string name, int attackBonus, int defenseBonus, string description, int price)
    {
        Name = name;
        AttackBonus = attackBonus;
        DefenseBonus = defenseBonus;
        Description = description;
        Price = price;
        IsEquipped = false;
    }

    public override string ToString()
    {
        return $"{Name} | 공격력 +{AttackBonus} | 방어력 +{DefenseBonus} | {Description} | {Price} G";
    }
}
