import React, { useState, useEffect } from "react";
import { LikeApi } from "../../api/index";

const styles = {
  countSpanStyle: {
    marginLeft: "5px",
    fontSize: "28px",
  },
  button: {
    display: "flex",
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "flex-end",
  },
};

const LikeBtn = (props) => {
  const [likesCount, setLikesCount] = useState(0);
  const [isLoading, setisLoading] = useState(false);

  var getArticleLikes = (articleId) => {
    setisLoading(true);

    LikeApi.get(`/Like/${articleId}`).then((c) => {
      setLikesCount(c.data.likesCount);
      setisLoading(false);
    });
  };

  var doLike = () => {
    setisLoading(true);

    LikeApi.post("/Like", { articleId: props.articleId }).then(() => {
      setLikesCount(likesCount + 1);
      setisLoading(false);
    });
  };

  useEffect(() => {
    getArticleLikes(props.articleId);
  }, [props.articleId]);

  return (
    <>
      <div style={styles.button}>
        <div>
          <button
            className={`button is-link ${isLoading ? "is-loading" : ""}`}
            type="button"
            onClick={doLike}
          >
            <span className="icon is-small">
              <i className="far fa-thumbs-up"></i>
            </span>
            <span>Like Me</span>
          </button>
        </div>
        {likesCount > 0 && (
          <div>
            <span style={styles.countSpanStyle}>{likesCount}</span>
          </div>
        )}
      </div>
    </>
  );
};

export default LikeBtn;
