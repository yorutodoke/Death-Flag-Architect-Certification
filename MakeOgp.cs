using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

class MakeOgp
{
    struct RankInfo
    {
        public string slug;
        public string rank;
        public string title;
        public string color;
        public string desc;
        public string sealTop;
        public string sealBottom;
    }

    static void Main()
    {
        var ranks = new RankInfo[]
        {
            new RankInfo { slug = "a", rank = "RANK A", title = "神域の一級死亡フラグ建築士", color = "#ff3355", desc = "あらゆる絶望で完璧なフラグを建立！\n映画開始45分以内の美しい退場が100%保証されています。", sealTop = "神域", sealBottom = "一級" },
            new RankInfo { slug = "b", rank = "RANK B", title = "熟練の死亡フラグ棟梁", color = "#ff7b36", desc = "定番フラグを完璧に熟知！\nクライマックス直前に仲間を逃がして散る名バイプレイヤー。", sealTop = "熟練", sealBottom = "棟梁" },
            new RankInfo { slug = "c", rank = "RANK C", title = "一人前のフラグ大工", color = "#ffbe2e", desc = "油断や慢心で程よく盛り上げて散る一人前のフラグ職人。", sealTop = "一人前", sealBottom = "職人" },
            new RankInfo { slug = "d", rank = "RANK D", title = "見習いフラグ職人", color = "#a3e635", desc = "生への未練が捨てきれず最終回の病室で包帯ぐるぐる巻きで生き残るタイプ。", sealTop = "見習", sealBottom = "職人" },
            new RankInfo { slug = "e", rank = "RANK E", title = "素人日曜大工", color = "#38bdf8", desc = "冷静になりすぎてフラグが建つ前に自分でへし折ってしまう地味なポジション。", sealTop = "素人", sealBottom = "生存" },
            new RankInfo { slug = "f", rank = "RANK F", title = "無自覚なフラグ解体業者", color = "#818cf8", desc = "周りがせっかく建てたフラグまで片っ端からへし折っていくクラッシャー。", sealTop = "解体", sealBottom = "業者" },
            new RankInfo { slug = "none", rank = "何しに来た？", title = "冷酷無比な完全生存リアリスト", color = "#94a3b8", desc = "【検定失格！】ドラマや様式美よりも己の命を最優先し、開始5分でタクシーで帰宅する超現実主義者。", sealTop = "検定", sealBottom = "失格" }
        };

        Directory.CreateDirectory("images");

        Font fontBadge = new Font("Meiryo", 19, FontStyle.Bold);
        Font fontGrade = new Font("Meiryo", 62, FontStyle.Bold);
        Font fontTitle = new Font("Meiryo", 28, FontStyle.Bold);
        Font fontDesc = new Font("Meiryo", 18, FontStyle.Regular);
        Font fontFooter = new Font("Meiryo", 20, FontStyle.Bold);
        Font fontStamp = new Font("Meiryo", 15, FontStyle.Bold);

        Color gold = Color.FromArgb(255, 184, 46);
        Brush goldBrush = new SolidBrush(gold);
        Brush whiteBrush = new SolidBrush(Color.White);
        Brush grayBrush = new SolidBrush(Color.FromArgb(205, 218, 235));
        Brush stampBrush = new SolidBrush(Color.FromArgb(255, 68, 68));

        StringFormat sfCenter = new StringFormat { Alignment = StringAlignment.Center };

        foreach (var r in ranks)
        {
            using (Bitmap bmp = new Bitmap(1200, 630))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                // 背景
                using (LinearGradientBrush bg = new LinearGradientBrush(new Rectangle(0, 0, 1200, 630), Color.FromArgb(10, 14, 22), Color.FromArgb(24, 30, 46), 45f))
                {
                    g.FillRectangle(bg, 0, 0, 1200, 630);
                }

                // 上部ハザードライン
                using (Pen hp = new Pen(gold, 10)) { g.DrawLine(hp, 0, 5, 1200, 5); }

                // 外枠
                using (Pen gp = new Pen(gold, 5)) { g.DrawRectangle(gp, 30, 30, 1140, 570); }
                using (Pen ip = new Pen(Color.FromArgb(70, 255, 184, 46), 2)) { g.DrawRectangle(ip, 44, 44, 1112, 542); }

                // ヘッダータイトル（豆腐化する絵文字を排除し、スタイリッシュなテキストに）
                g.DrawString("全日本フラグ建築業協会 技能検定結果通知書", fontBadge, goldBrush, 600, 75, sfCenter);
                g.DrawString("【 技 能 判 定 ラ ン ク 】", fontBadge, goldBrush, 600, 125, sfCenter);

                // ランク大文字
                Color rankColor = ColorTranslator.FromHtml(r.color);
                using (Brush rb = new SolidBrush(rankColor))
                {
                    g.DrawString(r.rank, fontGrade, rb, 600, 168, sfCenter);
                }

                // 称号
                g.DrawString(r.title, fontTitle, whiteBrush, 600, 280, sfCenter);

                // 説明文ボックス
                Rectangle boxRect = new Rectangle(120, 345, 960, 135);
                using (Brush bb = new SolidBrush(Color.FromArgb(190, 13, 17, 26)))
                using (Pen bp = new Pen(Color.FromArgb(52, 64, 89), 2))
                {
                    g.FillRectangle(bb, boxRect);
                    g.DrawRectangle(bp, boxRect);
                }

                StringFormat sfBox = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(r.desc, fontDesc, grayBrush, new RectangleF(130, 350, 940, 125), sfBox);

                // 合格印スタンプ
                using (Pen sp = new Pen(Color.FromArgb(255, 68, 68), 3))
                {
                    g.DrawRectangle(sp, 1005, 455, 80, 80);
                    g.DrawString(r.sealTop, fontStamp, stampBrush, 1045, 467, sfCenter);
                    g.DrawString(r.sealBottom, fontStamp, stampBrush, 1045, 497, sfCenter);
                }

                // フッター
                g.DrawString("#死亡フラグ建築検定", fontFooter, goldBrush, 600, 535, sfCenter);

                string outPath = Path.Combine("images", "ogp_rank_" + r.slug + ".png");
                bmp.Save(outPath, ImageFormat.Png);
                Console.WriteLine("Generated: " + outPath);
            }
        }

        // メイン看板 ogp.png の生成
        using (Bitmap bmp = new Bitmap(1200, 630))
        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            using (LinearGradientBrush bg = new LinearGradientBrush(new Rectangle(0, 0, 1200, 630), Color.FromArgb(10, 14, 22), Color.FromArgb(35, 12, 17), 45f))
            {
                g.FillRectangle(bg, 0, 0, 1200, 630);
            }

            using (Pen hp = new Pen(gold, 10)) { g.DrawLine(hp, 0, 5, 1200, 5); }
            using (Pen gp = new Pen(gold, 5)) { g.DrawRectangle(gp, 30, 30, 1140, 570); }
            using (Pen ip = new Pen(Color.FromArgb(70, 255, 184, 46), 2)) { g.DrawRectangle(ip, 44, 44, 1112, 542); }

            Font fontMainTitle = new Font("Meiryo", 50, FontStyle.Bold);
            using (Brush rb = new SolidBrush(Color.FromArgb(255, 51, 85)))
            {
                g.DrawString("全日本フラグ建築業協会 公認", fontBadge, goldBrush, 600, 85, sfCenter);
                g.DrawString("死亡フラグ建築検定", fontMainTitle, rb, 600, 140, sfCenter);
                g.DrawString("― どんな窮地でも、芸術的な死亡フラグを建てられるか？ ―", fontDesc, grayBrush, 600, 240, sfCenter);
            }

            Rectangle boxRect = new Rectangle(120, 310, 960, 160);
            using (Brush bb = new SolidBrush(Color.FromArgb(190, 13, 17, 26)))
            using (Pen bp = new Pen(Color.FromArgb(52, 64, 89), 2))
            {
                g.FillRectangle(bb, boxRect);
                g.DrawRectangle(bp, boxRect);
            }

            g.DrawString("「この戦いが終わったら…」「ここは俺に任せて先に行け！」", fontTitle, goldBrush, 600, 335, sfCenter);
            g.DrawString("絶体絶命のセリフで、あなたの技能ランクを精密判定！", fontDesc, whiteBrush, 600, 405, sfCenter);

            g.DrawString("#死亡フラグ建築検定", fontFooter, goldBrush, 600, 535, sfCenter);

            bmp.Save("ogp.png", ImageFormat.Png);
            Console.WriteLine("Generated: ogp.png");
        }
    }
}

