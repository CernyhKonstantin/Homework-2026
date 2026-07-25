import {
    PrintMachine,
    News,
    NewsFeed
} from "./User.js";


// Task 1

const printer = new PrintMachine(
    "24px",
    "blue",
    "Arial"
);

printer.print("Hello from Print Machine!");


// Task 2

const news1 = new News(
    "JavaScript Update",
    "New JavaScript features were released.",
    ["javascript", "web", "programming"],
    new Date()
);

const news2 = new News(
    "Football Match",
    "Barcelona won the match yesterday.",
    ["football", "sport"],
    new Date(2026, 4, 5)
);


// Print single news

news1.print();

news2.print();


// Task 3

const feed = new NewsFeed();

feed.addNews(news1);

feed.addNews(news2);


// Show all news

console.log("All news:");

feed.showAllNews();


// News count

console.log("News count:", feed.newsCount);


// Search by tag

console.log("Search by tag: football");

console.log(feed.searchByTag("football"));


// Sort news

console.log("Sorted news:");

feed.sortByDate();

feed.showAllNews();


// Delete news

feed.removeNews(0);

console.log("After delete:");

feed.showAllNews();