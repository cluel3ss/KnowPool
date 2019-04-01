<?php
/**
 * Created by PhpStorm.
 * User: Xonshiz
 * Date: 02-11-2018
 * Time: 10:47 PM
 */

if ($_SERVER['REQUEST_METHOD'] === 'POST')
{
    require_once('libs/initialize.php');
    /*
     * Open a DB connection and start checking the fields whether they are empty or not.
     * If they're empty, return appropriate error.
     * Also, if the field is NOT empty and we're using that data, we need to escape the special chars to avoid SQL Injection.
     */
    $db = db_connect();

    $course_details_id = mysqli_real_escape_string($db, $_POST['coursedetail_id']);

    $query = "SELECT * FROM courseDetails WHERE cd_id = '$course_details_id'";
    // echo $query;
    $result = mysqli_query($db, $query);
    $my_json = array();

    if ($result->num_rows > 0)
    {
        $count  = 0;
        /* fetch associative array */
        while ($row = mysqli_fetch_assoc($result)) {

            echo(json_encode(array(
                'cd_id' => $row["cd_id"],
                'cd_a_id' => $row["cd_a_id"],
                'cd_c_id' => $row["cd_c_id"],
                'cd_title' => $row["cd_title"],
                'cd_keywords' => $row["cd_keywords"],
                'cd_desc' => $row["cd_desc"],
            )));
        }

        db_disconnect($db, $result);
        exit(0);
    }
    else
    {
        echo json_encode(array(
            'error_code' => '1',
            'message' => 'No Entry Found'
        ));
        db_disconnect($db, $result);
        exit(1);
    }
}
else
{
    echo "<html><body><h1>Method Not Allowed</h1></body></html>";
}

?>

