<?php
/**
 * Created by PhpStorm.
 * User: Xonshiz
 * Date: 02-11-2018
 * Time: 10:52 PM
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

    $course_id = mysqli_real_escape_string($db, $_POST['course_id']);
    $course_author_id = mysqli_real_escape_string($db, $_POST['course_a_id']);

    $query = "SELECT cd_title, cd_id FROM courseDetails WHERE cd_c_id = '$course_id' AND cd_a_id = '$course_author_id'";
    // echo $query;
    $result = mysqli_query($db, $query);
    $my_json = array();

    if ($result->num_rows > 0)
    {
        $count = 0;
        /* fetch associative array */
        while ($row = mysqli_fetch_assoc($result)) {

            $my_json[$count] = array(
                'cd_id' => $row["cd_id"],
                'cd_title' => $row["cd_title"]
            );
            $count ++;
        }

        echo json_encode(array(
            'error_code' => '0',
            'message' => 'Entries Found',
            'records' => $my_json
        ));

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

