import "./App.css";
import Article from "./Article";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <Article articleId={1} />
        <Article articleId={2} />
      </header>
    </div>
  );
}

export default App;
