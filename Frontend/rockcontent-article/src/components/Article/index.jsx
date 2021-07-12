import LikeButton from "../LikeButton";

const styles = {
  articleStyle: {
    border: "1px solid",
    padding: "20px",
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
    alignItems: "center",
    margin: "15px",
  },
  headerStyle: {
    borderBottom: "1px solid",
    marginBottom: "15px",
  },
  sectionStyle: {
    minHeight: "30vh",
  },
};

const article = (props) => {
  return (
    <>
      <article style={styles.articleStyle}>
        <header style={styles.headerStyle}>I am the article header</header>
        <section style={styles.sectionStyle}>
          I am the article text.
          <p>Blablabla</p>
          Lorem Ipsum
        </section>
        <footer>
          <LikeButton articleId={props.articleId} />
        </footer>
      </article>
    </>
  );
};

export default article;
