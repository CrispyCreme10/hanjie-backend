USE hanjie;
CREATE TABLE cell (
    row_no TINYINT NOT NULL,
    col_no TINYINT NOT NULL,
    value TINYINT NOT NULL,
    PRIMARY KEY(row_no, col_no, value)
)