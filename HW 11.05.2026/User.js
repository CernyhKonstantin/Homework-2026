// Task 1

class PrintMachine {

    constructor(fontSize, fontColor, fontFamily) {

        this.fontSize = fontSize;

        this.fontColor = fontColor;

        this.fontFamily = fontFamily;
    }

    print(text) {

        document.write(`
            <p style="
                font-size:${this.fontSize};
                color:${this.fontColor};
                font-family:${this.fontFamily};
            ">
                ${text}
            </p>
        `);
    }
}


// Task 2

class News {

    constructor(title, text, tags, publishDate) {

        this.title = title;

        this.text = text;

        this.tags = tags;

        this.publishDate = publishDate;
    }


    getFormattedDate() {

        const now = new Date();

        const diff = now - this.publishDate;

        const days = Math.floor(
            diff / (1000 * 60 * 60 * 24)
        );

        if (days < 1) {

            return "Today";
        }

        if (days < 7) {

            return `${days} days ago`;
        }

        return this.publishDate.toLocaleDateString();
    }


    print() {

        console.log("---------------");

        console.log("Title:", this.title);

        console.log("Text:", this.text);

        console.log("Tags:", this.tags.join(", "));

        console.log("Date:", this.getFormattedDate());
    }
}


// Task 3

class NewsFeed {

    constructor() {

        this.newsArray = [];
    }


    get newsCount() {

        return this.newsArray.length;
    }


    showAllNews() {

        this.newsArray.forEach((news) => {

            news.print();
        });
    }


    addNews(news) {

        this.newsArray.push(news);
    }


    removeNews(index) {

        this.newsArray.splice(index, 1);
    }


    sortByDate() {

        this.newsArray.sort((a, b) => {

            return b.publishDate - a.publishDate;
        });
    }


    searchByTag(tag) {

        return this.newsArray.filter((news) => {

            return news.tags.includes(tag);
        });
    }
}


export {
    PrintMachine,
    News,
    NewsFeed
};